using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Services.MangakaService;

namespace WebApplication2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class MangakaController : ControllerBase
    {
        private readonly IMangakaService _mangakaService;
        public MangakaController(IMangakaService mangakaService)
        {
            _mangakaService = mangakaService;
        }

        [HttpGet("GetMangakaById")]
        public async Task<ActionResult> GetMangakaById(int id)
        {
            try
            {
                var mangaka = await _mangakaService.GetMangakaById(id);
                return Ok(mangaka);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetMangakas")]
        public async Task<ActionResult> GetAllMangakas()
        {
            try
            {
                var mangakas = await _mangakaService.GetAllMangakas();
                return Ok(mangakas);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteAnimeFromMangaka")]
        public async Task<ActionResult> DeleteAnimeFromMangaka(int mangakaId, int animeId)
        {
            try
            {
                var mangakas = await _mangakaService.DeleteAnimeFromMangaka(mangakaId, animeId);
                return Ok(mangakas);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteMangakaById")]
        public async Task<ActionResult> DeleteMangakaById(int id)
        {
            try
            {
                var mangakas = await _mangakaService.DeleteMangakaById(id);
                return Ok(mangakas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddAnimeToMangaka")]
        public async Task<ActionResult> AddAnimeToMangaka(int id, string anime)
        {
            try
            {
                var mangaka = await _mangakaService.AddAnimeToMangaka(id, anime);
                return Ok(mangaka);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateMangaka")]
        public async Task<ActionResult> UpdateMangaka(int id,  MangakaModel mangakaModel)
        {
            try
            {
                var mangaka = await _mangakaService.UpdateMangaka(id, mangakaModel);
                return Ok(mangaka);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
