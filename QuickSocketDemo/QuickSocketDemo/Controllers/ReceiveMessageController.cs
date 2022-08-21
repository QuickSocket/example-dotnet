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
    public class ReceiveMessageController : ControllerBase
    {
        private readonly IQuickSocketApi _quickSocketApi;
        private readonly IStore _store;
        private readonly IQuickSocketCallbackVerifier _quickSocketAuth;

        public ReceiveMessageController(
            IStore store, 
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
                    return;

                case "DISCONNECT":
                    _store.RemoveFromConnectionIds(model.ConnectionId);
                    return;

                case "MESSAGE":
                    var connectionIds = _store.GetConnectionIds();
                    await MessageReceivedHandler(connectionIds, model.Payload);
                    return;

                default:
                    return;
            }
        }

        private Task MessageReceivedHandler(List<string> connectionIds, string payload) 
        {
            var tasks = connectionIds.Select(x => _quickSocketApi.SendAsync(new SendRequestModel
            {
                ConnectionId = x,
                Payload = payload
            }));

            return Task.WhenAll(tasks);
        }
    }
}
