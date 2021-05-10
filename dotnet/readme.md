# DotNetCore Jwt Sdk

## Prerequisites

These samples require [.Net Core 5.0](https://dotnet.microsoft.com/download) to be installed.

The following libraries are needed to run the api:
    <ol>
		<li>Microsoft.IdentityModel.Tokens</li>
		<li>System.IdentityModel.Tokens.Jwt</li>
	</ol>

The solution include all necessary packages to run the samples.	

## Samples

The following sections describe the .Net samples included in
this SDK.

### 1. JWT
To generate a JWT execute the following steps:

<ol>
 <li>Open the C# solution and Build the solution first.</li>
 <li>Navigate to the folder path:
      ..\userPath\cjissdk\dotnet\dotnetClient\dotnetClient\bin\Debug\netcoreapp3.1
 </li>
 <li>In a command prompt execute dotnetClient.exe</li>
 <li>Choose option 1 to generate the JWT token.</li>
</ol>

### 2. Retrieve a Charge
This is a simple console application that creates a request that calls the api client to retrieve a single charge code.

<ol>
   <li>Open the C# solution and Build the solution first. If you have not already done so.</li>
	<li> Populate the APIKEY environment variable with a valid API key.</li>
	<li> Execute the dotnetClient.exe and chose option 2.</li>
</ol>

### 3. System to System
The system-to-system sample only works if the target environment is configured to allow the myidp issuer. Otherwise this returns a 401 HTTP status code.