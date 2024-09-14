using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication2.V2.Services;

namespace WebApplication2.V2.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/test")]
    [Authorize]
    public class TestVersionController : ControllerBase
    {
        private readonly ITestVersionServiceV2 _testVersionService;
        public TestVersionController(ITestVersionServiceV2 testVersionService)
        {
            _testVersionService = testVersionService;
        }
        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            string result = await _testVersionService.Get();
            return Ok(result);
        }
    }
}
