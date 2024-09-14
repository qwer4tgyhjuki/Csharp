
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.V2.Services
{
    public class TestVersionServiceV2 : ITestVersionServiceV2
    {
        public Task<string> Get()
        {
            return Task.FromResult("Hello World");
        }
    }
}
