using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using QuickSocketDemo.Auth;
using QuickSocketDemo.Http;
using QuickSocketDemo.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuickSocketDemo.Controllers
{
    // Responsible for handling messages received from the QuickSocket server.
    [Route("api/receive")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IQuickSocketApi _quickSocketApi;
        private readonly IRepository _store;
        private readonly IQuickSocketCallbackVerifier _quickSocketAuth;

        public CallbackController(
            IRepository store, 
            IQuickSocketApi quickSocketApi,
            IQuickSocketCallbackVerifier quickSocketAuth)
        {
            _store = store;
            _quickSocketApi = quickSocketApi;
            _quickSocketAuth = quickSocketAuth;
        }

        [HttpPost]
        public async Task Handle()
        {
            // We read the request body here manually and not using
            // [FromBody] because we need to verify the signature across
            // the raw body of the callback.
            using var streamReader = new StreamReader(Request.Body);
            var rawBody = await streamReader.ReadToEndAsync();
            var model = JsonConvert.DeserializeObject<ReceiveMessageRequestModel>(rawBody);
            var action = model.Action;

            if (!_quickSocketAuth.IsVerified(
                Request.Headers["QS-Auth-Token-1"], 
                Request.Headers["QS-Auth-Token-2"], 
                Request.Headers["QS-Signature"],
                rawBody
                ))
            {
                return;
            }

            switch (action)
            {
                case "CONNECT":
                    _store.AddToConnectionIds(model.ConnectionId);
                    break;

                case "DISCONNECT":
                    _store.RemoveFromConnectionIds(model.ConnectionId);
                    break;

                case "MESSAGE":
                    var connectionIds = _store.GetConnectionIds();
                    await MessageReceivedHandler(connectionIds, model.Payload);
                    break;
            }
        }

        private Task MessageReceivedHandler(List<string> connectionIds, string payload) 
        {
            var tasks = connectionIds.Select(x => _quickSocketApi.SendAsync(x, payload));

            return Task.WhenAll(tasks);
        }
    }
}
