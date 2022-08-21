using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using QuickSocketDemo.Settings;
using System.Text;

namespace QuickSocketDemo.Auth
{
    // The QuickSocketCallbackVerifier is responsible for ensuring that
    // requests that come from the QuickSocket server are valid.
    public class QuickSocketCallbackVerifier : IQuickSocketCallbackVerifier
    {
        private readonly IQuickSocketSettings _quickSocketSettings;

        public QuickSocketCallbackVerifier(IQuickSocketSettings quickSocketSettings)
        {
            _quickSocketSettings = quickSocketSettings;
        }

        // This method checks that at least one of the provided auth tokens are valid
        // and that the signature for the request body is valid.
        public bool IsVerified(string authToken1, string authToken2, string signature, string requestBody)
        {
            if (authToken1 != _quickSocketSettings.AuthToken1 && authToken2 != _quickSocketSettings.AuthToken2)
            {
                return false;
            }
 
            var rawRequestBody = Encoding.UTF8.GetBytes(requestBody);
            var rawSignature = Base64UrlEncoder.DecodeBytes(signature);
            var rawPublicKey = Base64UrlEncoder.DecodeBytes(_quickSocketSettings.SignaturePublicKey);
            var publicKeyParameters = new Ed25519PublicKeyParameters(rawPublicKey);

            var verifier = new Ed25519Signer();
            verifier.Init(forSigning: false, publicKeyParameters);
            verifier.BlockUpdate(rawRequestBody, 0, rawRequestBody.Length);
            return verifier.VerifySignature(rawSignature);
        }
    }
}
