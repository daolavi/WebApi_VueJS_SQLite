namespace WebApplication.Services.AuthenticationService
{
    public interface IAuthenticationService
    {
        Models.User Authenticate(string username, string password);

        string BuildToken(Models.User account);
    }
}
