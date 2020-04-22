using System;
using io.fusionauth;
using io.fusionauth.domain;
using io.fusionauth.domain.api;
using io.fusionauth.domain.api.user;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace usermanager
{
    class Program
    {
        private static readonly string apiKey = Environment.GetEnvironmentVariable("fusionauth_api_key");
        private static readonly string fusionauthURL = "http://localhost:9011";

	private static readonly string tenantId = "66636432-3932-3836-6630-656464383862";
	private static readonly string applicationId = "4243b56f-0b45-4882-aa23-ac75eea22d22";

        static void Main(string[] args)
        {
	    if (args.Length != 3) {
                Console.WriteLine("Please provide email, password and favorite color.");
		Environment.Exit(1);
	    }
            string email= args[0];
            string password = args[1];
            string favoriteColor = args[2];

            FusionAuthSyncClient client = new FusionAuthSyncClient(apiKey, fusionauthURL, tenantId);

	    var userRequest = buildUserRequest(email, password, favoriteColor);
            var response = client.CreateUser(null, userRequest);
	    // debugging
	    //string json = JsonConvert.SerializeObject(response);
            //Console.WriteLine(json);

            if (response.WasSuccessful())
            {
                var user = response.successResponse.user;
		var registrationResponse = register(client, user);
		if (registrationResponse.WasSuccessful()) {
                    Console.WriteLine("created user with email: "+user.email);
		} 
                else if (registrationResponse.statusCode != 200) 
                {
                    var statusCode = registrationResponse.statusCode;
                    Console.WriteLine("failed with status "+statusCode);
	            string json = JsonConvert.SerializeObject(response);
                    Console.WriteLine(json);
                } 
            } 
            else if (response.statusCode != 200) 
            {
                var statusCode = response.statusCode;
                Console.WriteLine("failed with status "+statusCode);
	        string json = JsonConvert.SerializeObject(response);
                Console.WriteLine(json);
            } 
        }

        static UserRequest buildUserRequest(string email, string password, string favoriteColor)
	{
	    User userToCreate = new User();
	    userToCreate.email = email;
	    userToCreate.password = password;
	    Dictionary<string, object> data = new Dictionary<string, object>();
	    data.Add("favoriteColor", favoriteColor);
	    userToCreate.data = data;

	    UserRequest userRequest = new UserRequest();
	    userRequest.sendSetPasswordEmail = false;
	    userRequest.user = userToCreate;
	    return userRequest;
	}

        static ClientResponse<RegistrationResponse> register(FusionAuthSyncClient client, User user)
        {
	    RegistrationRequest registrationRequest = new RegistrationRequest();
	    UserRegistration registration = new UserRegistration();
	    registration.applicationId = Guid.Parse(applicationId);
	    registrationRequest.sendSetPasswordEmail = false;
	    registrationRequest.skipRegistrationVerification = true;
	    registrationRequest.skipVerification = true;
	    registrationRequest.registration = registration;
            return client.Register(user.id, registrationRequest);
        }
    }
}
