using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.FileExplorer.Api.Models.DTOs;
using Training.FileExplorer.Application.FileStorage.Services;

namespace Training.FileExplorer.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrivesController : ControllerBase
    {
        private readonly IMapper _mapper;

        public DrivesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetAsync([FromServices] IDriveService driveService)
        {
            var data = await driveService.GetAsync();
            var result = _mapper.Map<IEnumerable<StorageDriveDto>>(data);
            return result.Any() ? Ok(result) : NoContent();
        }
    }
}
