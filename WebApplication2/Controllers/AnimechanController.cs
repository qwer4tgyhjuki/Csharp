using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.IO;
using WebApplication2.Models;
using WebApplication2.Services.ApiService;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimechanController : ControllerBase
    {
        readonly private IApiService _apiService;
        public AnimechanController(IApiService apiService)
        {
            _apiService = apiService;

        }

        [HttpGet("RandomQuote")]
        public async Task<ActionResult> GetRandomQuote()
        {
            var responseModel = new ResponseModel<AnimeRandomQuoteModel?>();
            try
            {
                responseModel.Data = await _apiService.GetApi<AnimeRandomQuoteModel>("https://animechan.xyz/api/random");
                responseModel.StatusCode = HttpStatusCode.OK;
                responseModel.Message = "Success! COOL!";
                return Ok(responseModel);
            }
            catch (Exception ex)
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = ex.Message;
                return StatusCode((int)responseModel.StatusCode,
                    responseModel.Message);
            }
        }
        [HttpPost("PostRandomQuote")]
        public async Task<ActionResult> PostAnime()
        {
            var responseModel = new ResponseModel<AnimeRandomQuoteModel?>();
            var resultModel = new LocalAnimeModel();
            try
            {
                responseModel.Data = 
                    await _apiService.GetApi<AnimeRandomQuoteModel>("https://animechan.xyz/api/random");
                resultModel.Id = responseModel.Data.Id;
                resultModel.Name = responseModel.Data.Anime;

                var jsonResult = JsonSerializer.Serialize(resultModel);
                string path = "resultModel.json";

                using (StreamWriter file = new StreamWriter(path, append: true))
                {
                    if(file.BaseStream.Length != 0)
                    {
                        await file.WriteLineAsync(",");
                    }
                    await file.WriteAsync(jsonResult);
                }
                return Ok(resultModel);
            } catch (Exception ex)
            {
                responseModel.StatusCode = HttpStatusCode.InternalServerError;
                responseModel.Message = ex.Message;
                return StatusCode((int)responseModel.StatusCode,
                    responseModel.Message);
            }
        }
    }
}
