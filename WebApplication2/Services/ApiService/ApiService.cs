namespace WebApplication2.Services.ApiService
{
    public class ApiService : IApiService
    {
        readonly private HttpClient _httpClient;
        public ApiService(HttpClient httpClient) 
        { 
            _httpClient = httpClient;
        }
        public async Task<T?> GetApi<T>(string? url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url); 
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
