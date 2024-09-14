using Microsoft.AspNetCore.Mvc;
using WebApplication2.V3.Models;

namespace WebApplication2.V3.Services
{
    public interface ITestVersionServiceV3
    {
        public Task<ExcelModel> Get();
    }
}
