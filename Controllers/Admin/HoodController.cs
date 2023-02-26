using AutoMapper;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Admin.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bgbrokersapi.Controllers.Admin
{
    [Route("/Hood")]
    [ApiController]
    public class HoodController : ControllerBase
    {
        private readonly ILocationService locationService;
        private readonly IMapper mapper;

        public HoodController(ILocationService locationService,
            IMapper mapper)
        {
            this.locationService = locationService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHood(HoodInputModel model)
        {
            var response = await locationService.CreateHood(model);

            if (response.Status == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Hood.Id }, response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = new HoodResponseModel();

            try
            {
                var hood = await locationService.GetHoodById(id);

                response.Status = StatusCodes.Status200OK;
                response.Hood = mapper.Map<HoodModel>(hood);
            }
            catch (Exception e)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = e.Message;

                return NotFound(response);
            }

            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new AllHoodsResponseModel();

            var hoods = await locationService.GetAllHoods();

            response.Status = StatusCodes.Status200OK;
            response.Hoods = mapper.Map<IEnumerable<HoodModel>>(hoods);

            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await locationService.DeleteHood(id);

            if (response.Status == 400)
            {
                return BadRequest(response);
            }
            else if (response.Status == 404)
            {
                return NotFound(response);
            }

            return Accepted(response);
        }
    }
}
