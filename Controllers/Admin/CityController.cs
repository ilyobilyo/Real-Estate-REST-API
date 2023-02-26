using AutoMapper;
using bgbrokersapi.Data.Models.OfferLocation;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Admin.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bgbrokersapi.Controllers.Admin
{
    [Route("/City")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ILocationService locationService;
        private readonly IMapper mapper;

        public CityController(ILocationService locationService,
            IMapper mapper) 
        {
            this.locationService = locationService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCity(TypeInputModel model)
        {
            var response = await locationService.CreateCity(model);

            if (response.Status == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.City.Id }, response);
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = new CityResponseModel();

            try
            {
                var city = await locationService.GetCityById(id);

                response.Status = StatusCodes.Status200OK;
                response.City = mapper.Map<CityModel>(city);
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
            var response = new AllCitiesResponseModel();

            var cities = await locationService.GetAllCities();

            response.Status = StatusCodes.Status200OK;
            response.Cities = mapper.Map<IEnumerable<CityModel>>(cities);

            return Ok(response);
        }
    }
}
