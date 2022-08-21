namespace QuickSocketDemo.Settings
{
    public interface IQuickSocketSettings
    {
        string ClientId { get; }
        string SignaturePublicKey { get; }
        string ClientSecret1 { get; }
        string AuthToken1 { get; }
        string AuthToken2 { get; }
    }
}
