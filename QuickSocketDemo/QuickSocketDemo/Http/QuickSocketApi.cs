using Newtonsoft.Json;
using QuickSocketDemo.Models;
using QuickSocketDemo.Settings;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace QuickSocketDemo.Http
{
    // The QuickSocketApi is responsible for communication to the QuickSocket Management API..
    public class QuickSocketApi : IQuickSocketApi
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IQuickSocketSettings _quickSocketSettings;
        
        public QuickSocketApi(IHttpClientFactory httpClientFactory, IQuickSocketSettings quickSocketSettings)
        {
            _httpClientFactory = httpClientFactory;
            _quickSocketSettings = quickSocketSettings;
        }

        public Task SendAsync(string connectionId, string payload)
        {
            return PerformPostRequestAsync("https://manage.quicksocket.io/send", new SendRequestModel
            {
                ConnectionId = connectionId,
                Payload = payload
            });
        }

        public async Task<string> AuthAsync(string referenceId)
        {
            var res = await PerformPostRequestAsync("https://manage.quicksocket.io/auth", new AuthenticateConnectionRequestModel
            {
                ReferenceId = referenceId
            });

            var output = await res.Content.ReadAsStringAsync();

            return JsonConvert
                .DeserializeObject<CreateConnectionResponseModel>(output)
                .ConnectionToken;
        }

        private async Task<HttpResponseMessage> PerformPostRequestAsync<T>(string url, T requestModel)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var req = new HttpRequestMessage(HttpMethod.Post, url);
            var json = JsonConvert.SerializeObject(requestModel);

            req.Content = new StringContent(json);
            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            req.Headers.Authorization =
                new BasicAuthenticationHeaderValue(_quickSocketSettings.ClientId, _quickSocketSettings.ClientSecret1);

            var res = await httpClient.SendAsync(req);
            res.EnsureSuccessStatusCode();

            return res;
        }
    }
}
