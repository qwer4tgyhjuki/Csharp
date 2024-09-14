using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.V2.Services
{
    public interface ITestVersionServiceV2
    {
        public Task<string> Get();
    }
}
