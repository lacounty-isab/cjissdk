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
            }         
        }

        public static void GetToken()
        {
            // Define const Key this should be private secret key  stored in some safe place
            string key = Environment.GetEnvironmentVariable("JWT_SECRET");

            // Create Security key  using private key above:
            // not that latest version of JWT using Microsoft namespace instead of System
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            // Also note that securityKey length should be >256b
            // so you have to make sure that your private key has a proper length
            //
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
                              (securityKey, SecurityAlgorithms.HmacSha256Signature);

            //  Finally create a Token
            var header = new JwtHeader(credentials);

            //Some PayLoad that contain information about the  customer
            var payload = new JwtPayload
           {
               { "user", "e123123"},
               { "scope", "da, lausd"},
           };

            //
            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();

            // Token to String so you can use it in your client
            var tokenString = handler.WriteToken(secToken);

            Console.WriteLine(tokenString);
            Console.WriteLine();
            Console.WriteLine("Consume Token");
            // And finally when  you received token from client
            // you can  either validate it or try to  read
            var token = handler.ReadJwtToken(tokenString);

            Console.WriteLine(token.Payload.First().Value);
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
    }
}
