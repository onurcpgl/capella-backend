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
        public async Task<IActionResult> AddTag([FromBody] TagDto tagDto)
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
    }
}
