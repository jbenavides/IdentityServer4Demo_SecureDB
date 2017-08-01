# IdentityServer4Demo_SecureDB

### Main Packages to add:
- IdentityServer.EntityFramework
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools

### Running migrations

dotnet ef migrations add InitialPersistedGrant -c PersistedGrantDbContext -o Migrations/IdentityServer/PersistedGrantDb
dotnet ef migrations add initialConfiguration -c ConfigurationDbContext -o Migrations/IdentityServer/ConfigurationDb


dotnet ef database update -c PersistedGrantDbContext
dotnet ef database update -c ConfigurationDbContext
