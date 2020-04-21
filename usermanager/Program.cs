using System;
using io.fusionauth;

namespace usermanager
{
    class Program
    {
        private static readonly string apiKey = Environment.GetEnvironmentVariable("fusionauth_api_key");
        private static readonly string fusionauthURL = "http://localhost:9011";

        static void Main(string[] args)
        {
            FusionAuthSyncClient client = new FusionAuthSyncClient(apiKey, fusionauthURL);
            var response = client.RetrieveUserByEmail("dotnetcore@example.com");
            if (response.WasSuccessful())
            {
                var user = response.successResponse.user;
                Console.WriteLine("retrieved user with email: "+user.email);
            } 
            else if (response.statusCode != 200) 
            {
                var statusCode = response.statusCode;
                Console.WriteLine("failed with status "+statusCode);
            } 
        }
    }
}
