using Application.DataTransferObject;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpPost("/tag")]
        public async Task<IActionResult> Save([FromBody] TagDto tagDto)
        {
            var result = await _tagService.Save(tagDto);
            if (!result)
            {
                return BadRequest();
            }
            else
            {
                return Ok(true);
            }

        }

        [HttpGet("/tags")]
        public async Task<IActionResult> GetTags()
        {
            var result = await _tagService.GetAllTags();
            return Ok(result);
        }

        [HttpGet("/tags/{code}")]
        public async Task<ActionResult> GetTagByCode([FromRoute] string code)
        {
            var result = await _tagService.GetTagByCode(code);
            return Ok(result);
        }
    }
}
