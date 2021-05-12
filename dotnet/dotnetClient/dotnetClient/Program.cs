using System;
using System.Net.Http;
using System.Text;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace dotnetClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Please enter one of the following selections to run: ");
            Console.WriteLine("\t 1. Jwt \n\t 2. Retrieve a Charge \n\t 3. System to System");

            Console.WriteLine("\n Please enter your selection:");
            int userSelection = Int32.Parse(Console.ReadLine());
            switch (userSelection)
            {
                case 1:
                    GetToken();
                    break;
                case 2:
                    await getCharge();
                    break;
                case 3:
                    await updateCharge();
                    break;
            }
        }

        public static String GetToken()
        {
            // Define const Key this should be private secret key stored in some safe place
            string key = Environment.GetEnvironmentVariable("CJISSDK_SECRET");

            // Create Security key using private key above:
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256);

            //  Finally create a Token
            var header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the  customer
            var scopes = new[] { "shortdesc" };
            DateTime today = DateTime.UtcNow;
            int iat = (int)(today - new DateTime(1970, 1, 1)).TotalSeconds;
            int exp = (int)(today.AddMinutes(100) - new DateTime(1970,1,1)).TotalSeconds;
            var payload = new JwtPayload
            {
               { "iss", "workshop"},
               { "sub", "e654321" },
                { "aud", "cjisapi" },
                { "iat", iat },
                { "exp", exp },
                { "enabled", true },
               { "scope", scopes}
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);

            Console.WriteLine(tokenString);
            // And finally when you received token from client
            // you can  either validate it or try to  read
            var token = handler.ReadJwtToken(tokenString);

            Console.WriteLine(token.Payload.First().Value);

            return tokenString;
        }
        public static async Task getCharge()
        {
            int id = 826;
            var testAPI = "https://api-test.codes.lacounty-isab.org";
            var path = String.Format("api/ChargeCode/{0}", id);
            var apiKey = Environment.GetEnvironmentVariable("APIKEY");
            Uri uriAddress = new Uri(testAPI + "/" + path);
          
            using (var client = new HttpClient { BaseAddress = uriAddress })
            {
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(uriAddress);
                var result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result.Replace("[", "").Replace("]", ""));
                Console.WriteLine(result);
            }
        }

        public static async Task updateCharge()
        {
            var testAPI = "https://api-test.codes.lacounty-isab.org/api/ChargeCode";
            var apiKey = Environment.GetEnvironmentVariable("APIKEY");
            Uri uriAddress = new Uri(testAPI);

            var bearerToken = GetToken();
            var id = 826;
            var short_desc = "UNLAWFUL LIQUOR SALES 2";
            var stringPayload = JsonConvert.SerializeObject(new { 
                id = id,
                short_description = short_desc
            });
            var content = new StringContent(stringPayload, Encoding.UTF8, "application/json");
            Console.WriteLine(content.ReadAsStringAsync());
            using (var client = new HttpClient { BaseAddress = uriAddress })
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
                client.DefaultRequestHeaders.Add("x-api-key", apiKey);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.PatchAsync(uriAddress.ToString(), content);
                var result = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine(result);
            }
        }
    }
    public class JsonData
    {
        public int id { get; set; }
        public string short_description { get; set; }

        public JsonData()
        {

        }
        public JsonData(int id, string short_description)
        {
            this.id = id;
            this.short_description = short_description;
        }
    }
}
