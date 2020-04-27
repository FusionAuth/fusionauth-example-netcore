# FusionAuth and .NET core 

An example of using the FusionAuth API with .NET core.

This application uses FusionAuth APIs to add a user and then search for users from the command line. This can be used to pull a subset of data from the FusionAuth database, for instance to get the assigned laptop number.

## To run

```
cd usermanager
fusionauth_api_key=<yourkey here> dotnet.exe run -- newuser@example.com mysecurepassword blue
```

Output:
```
created user with email: newuser@example.com
```

