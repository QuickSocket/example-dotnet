using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickSocketDemo.Http;
using QuickSocketDemo.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QuickSocketDemo.Controllers
{
    // Responsible for handling the authorisation of a new connection
    [Route("api/auth")]
    [ApiController]
    public class CreateConnectionController : ControllerBase
    {
        private readonly IQuickSocketApi _quickSocketApi;

        public CreateConnectionController(IQuickSocketApi quickSocketApi)
        {
            _quickSocketApi = quickSocketApi;
        }

        [HttpGet]
        public async Task<ActionResult<CreateConnectionResponseModel>> Handler()
        {
            var connectionToken = await _quickSocketApi.AuthAsync(new AuthenticateConnectionRequestModel
            {
                ReferenceId = "ref123" // Your own ID
            });

            return Ok(new CreateConnectionResponseModel
            {
                ConnectionToken = connectionToken
            });
        }
    }
}
