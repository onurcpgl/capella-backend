using API.Utilities.ResponseData;
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
        private readonly ILogger<TagController> _logger;
        public TagController(ITagService tagService, ILogger<TagController> logger)
        {
            _tagService = tagService;
            _logger = logger;
        }

        [HttpPost("/tag")]
        public async Task<IActionResult> Save([FromBody] TagDto tagDto)
        {
            _logger.LogInformation("Inside Save of TagController", tagDto);
            await _tagService.Save(tagDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS
            };
            return Ok(response);

        }

        [HttpGet("/tags")]
        public async Task<IActionResult> GetTags()
        {
            _logger.LogInformation("Inside GetTags of TagController");
            var tags = await _tagService.GetAllTags();
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = tags
            };
            return Ok(response);
        }

        [HttpGet("/tags/{code}")]
        public async Task<ActionResult> GetTagByCode([FromRoute] string code)
        {
            _logger.LogInformation("Inside GetTagByCode of TagController", code);
            var tag = await _tagService.GetTagByCode(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS,
                Data = tag
            };
            return Ok(response);
        }

        [HttpPut("/tags")]
        public async Task<IActionResult> Update([FromBody] TagDto tagDto)
        {
            _logger.LogInformation("Inside Update of TagController", tagDto);
            await _tagService.Update(tagDto);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }

        [HttpDelete("/tags/{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            _logger.LogInformation("Inside Delete of TagController", code);
            await _tagService.Delete(code);
            var response = new ServiceResponseData
            {
                Status = ProcessStatus.SUCCESS

            };
            return Ok(response);

        }
    }
}
