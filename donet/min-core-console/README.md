# Configure your Console project to use User Secrets

1. Generate a new Guid value and specify it in your .csproj as shown below. (You have to open your .csproj in an text editor)

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp2.1</TargetFramework>
    <UserSecretsId>YOURNEWGUID</UserSecretsId>
  </PropertyGroup>

# Creating a local secrets

For exmample, this project uses Azure Storag Account, which need secrets to configure client. Local secrets a stored under your %APPDATA% folder, based on your secret ID (the Guid you just created above).

1. Open a commandline or PowerShell in as Administrator
1. Run `dotnet user-secrets set DocumentDbUrl "https://YOURACCOUNTNAME.documents.azure.com:443/"`
1. Run `dotnet user-secrets set DocumentDbPrimaryKey "YOURPRIMARYKEYd9OA=="`
1. Using a text editor, open `%APPDATA%Roaming\Microsoft\UserSecrets\YOURNEWGUID\secrets.json`
1. Update the content of secrets.json to add MyWorkerSettings as shown below:

```
{
	"MyWorkerSettings":
	{
	"DocumentDbUrl": "https://YOURACCOUNTNAME.documents.azure.com:443/",
	"DocumentDbPrimaryKey": "YOURPRIMARYKEYd9OA=="
   }
}
```

Now, when you run this sample console application, it will automatically pulls the values from your local secret file, and make it available to Worker class.
