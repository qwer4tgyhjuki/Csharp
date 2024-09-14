using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.V1.Services
{
    public interface ITestVersionServiceV1
    {
        public Task<int> Get();
    }
}
