
namespace WebApplication2.V1.Services
{
    public class TestVersionServiceV1 : ITestVersionServiceV1
    {
        public Task<int> Get()
        {
            return Task.FromResult(new Random().Next(1,100));
        }
    }
}
