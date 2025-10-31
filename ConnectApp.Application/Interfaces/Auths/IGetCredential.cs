namespace ConnectApp.Application.Interfaces.Auths
{
    public interface IGetCredential
    {
        Guid GetUserId();
        string GetUserName();
        Guid GetAccountId();
        string GetAccountName();
    }
}
