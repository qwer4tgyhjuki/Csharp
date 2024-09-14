using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication2.V2.Services;
using WebApplication2.V3.Models;
using WebApplication2.V3.Services;

namespace WebApplication2.V3.Controllers
{
    [ApiController]
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/test")]
    [Authorize]
    public class TestVersionController : ControllerBase
    {
        private readonly ITestVersionServiceV3 _testVersionService;

        public TestVersionController(ITestVersionServiceV3 testVersionService)
        {
            _testVersionService = testVersionService;
        }

        [HttpGet]
        public async Task<ActionResult<ExcelModel>> Get()
        {
            ExcelModel result = await _testVersionService.Get();
            return Ok(result);
        }
    }
}