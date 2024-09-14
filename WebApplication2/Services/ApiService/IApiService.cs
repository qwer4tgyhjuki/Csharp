namespace WebApplication2.Services.ApiService
{
    public interface IApiService
    {
        public Task<T> GetApi<T>(string? url);
    }
}
