using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace GalacticViewWebAPI.Presentation.Controllers
{
    [Route("api/all-news")]
    [ApiController]
    public class AllNewsController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AllNewsController(IServiceManager service) => _service = service;

        [HttpGet(Name = "GetAllNews")]
        public async Task<IActionResult> GetAllNews()
        {
            var allNews = await _service.NewsService.GetAllNewsAsync(trachChanges: false);
            return Ok(allNews);
        }

        [HttpPost(Name = "CreateNews")]
        public async Task<IActionResult> CreateNews([FromBody] NewsForCreationDto news)
        {
            var createdNews = await _service.NewsService.CreateNewsAsync(news);

            return CreatedAtRoute("CreateNews", new { id = createdNews.Id }, createdNews);
        }
    }
}
