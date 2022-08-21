using Microsoft.Extensions.Configuration;

namespace QuickSocketDemo.Settings
{
    // QuickSocket settings layer
    public class QuickSocketSettings : IQuickSocketSettings
    {
        public QuickSocketSettings(IConfiguration configuration)
        {
            var section = configuration.GetSection("QuickSocket");
            ClientId = section.GetSection("ClientId").Get<string>();
            SignaturePublicKey = section.GetSection("SignaturePublicKey").Get<string>();
            ClientSecret1 = section.GetSection("ClientSecret1").Get<string>();
            AuthToken1 = section.GetSection("AuthToken1").Get<string>();
            AuthToken2 = section.GetSection("AuthToken2").Get<string>();
        }

        public string ClientId { get; }
        public string SignaturePublicKey { get; }
        public string ClientSecret1 { get; }
        public string AuthToken1 { get; }
        public string AuthToken2 { get; }
    }
}
