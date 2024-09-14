using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication2.V1.Services;

namespace WebApplication2.V1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/test")]
    [Obsolete("This version is deprecated.")]
    [Authorize]
    public class TestVersionController : ControllerBase
    {
        private readonly ITestVersionServiceV1 _testVersionService;
        public TestVersionController(ITestVersionServiceV1 testVersionService)
        {
            _testVersionService = testVersionService;
        }

        [HttpGet]
        [ApiVersion("1.0", Deprecated = true)]
        public async Task<ActionResult<int>> Get()
        {
            int result = await _testVersionService.Get();
            return Ok(result);
        }
    }
}
