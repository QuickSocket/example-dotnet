using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using QuickSocketDemo.Http;
using QuickSocketDemo.Models;
using System;
using System.Threading.Tasks;

namespace QuickSocketDemo.Controllers
{
    // Responsible for handling the authorisation of a new connection
    [Route("api/auth")]
    [ApiController]
    public class AuthConnectionController : ControllerBase
    {
        private readonly IQuickSocketApi _quickSocketApi;

        public AuthConnectionController(IQuickSocketApi quickSocketApi)
        {
            _quickSocketApi = quickSocketApi;
        }

        [HttpPost]
        public async Task<ActionResult<CreateConnectionResponseModel>> Handler()
        {
            // Normally you'd use a userId or something similar here,
            // but it doesn't matter for this example.
            var connectionToken = await _quickSocketApi.AuthAsync(Guid.NewGuid().ToString());

            return Ok(new CreateConnectionResponseModel
            {
                ConnectionToken = connectionToken
            });
        }
    }
}
