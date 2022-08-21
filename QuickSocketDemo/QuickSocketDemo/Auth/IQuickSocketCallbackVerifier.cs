namespace QuickSocketDemo.Auth
{
    public interface IQuickSocketCallbackVerifier
    {
        bool IsVerified(string authToken1, string authToken2, string signature, string requestBody);
    }
}
