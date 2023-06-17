# Observer

# Secret Manager setup
1. cd Observer.Head\Observer.Head.API
2. dotnet user-secrets init
3. dotnet user-secrets set "Auth0:Domain" "***" (Auth0 account ID)
4. dotnet user-secrets set "Auth0:ClientId" "***" (ClientId of Machine to Machine application)
5. dotnet user-secrets set "Auth0:ClientSecret" "***" (ClientSecret of Machine to Machine application)