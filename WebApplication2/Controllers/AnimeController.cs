using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services.AnimeService;
using WebApplication2.Services.ApiService.ApiService;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnimeController : ControllerBase
    {
        readonly private IAnimeService _animeService;

        public AnimeController(IAnimeService animeService)
        {
            _animeService = animeService;
        }

        [HttpGet("GetAnimeById")]
        public async Task<ActionResult> GetAnimeById(int id)
        {
            var animeModel = new AnimeModel();
            try
            {
                animeModel = await _animeService.GetAnimeById(id);
                Console.WriteLine(animeModel.Name);
                return Ok(animeModel);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAllAnime")]
        public async Task<ActionResult> GetAllAnime()
        {
            try
            {
                var animeList = await _animeService.GetAllAnime();
                return Ok(animeList);

            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddAnime")]
        public async Task<ActionResult> AddAnime(AnimeModel animeModel)
        {
            try
            {
                _animeService.AddAnime(animeModel);
                return Ok(animeModel);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateAnime")]
        public async Task<ActionResult> UpdateAnime(int id, AnimeModel animeModel)
        {
            try
            {
                var updatedAnime = await _animeService.UpdateAnime(id, animeModel);
                return Ok(new {Message = "Updated", updatedAnime = updatedAnime });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAnimeById")]
        public async Task<ActionResult> DeleteAnimeById(int id)
        {
            try
            {
                var updatedAnimeList = await _animeService.DeleteAnimeById(id);
                return Ok(new { Message =  "Deleted", AnimeList = updatedAnimeList});
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("PostRandomAnime")]
        public async Task<ActionResult> PostRandomAnime([FromServices] IApiService apiService)
        {
            var responseModel = new ResponseModel<AnimeRandomQuoteModel>();
            var animeModel = new AnimeModel();
            try
            {
                responseModel.Data =
                    await apiService.GetApi<AnimeRandomQuoteModel>("https://animechan.xyz/api/random");
                animeModel.Name = responseModel.Data.Anime;
                await _animeService.PostRandomAnime(animeModel);
                return Ok(animeModel);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
