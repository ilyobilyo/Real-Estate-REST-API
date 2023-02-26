using AutoMapper;
using bgbrokersapi.Data.Models.Types;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Admin.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bgbrokersapi.Controllers.Admin
{
    [Route("/OfferType")]
    [ApiController]
    public class OfferTypeController : TypeController
    {
        public OfferTypeController(ITypeService typeService,
             IMapper mapper)
            : base(typeService, mapper)
        {
        }

        [HttpPost]
        public async override Task<IActionResult> Create(TypeInputModel model)
        {
            var response = await typeService.Create<OfferType>(model);

            if (response.Status == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Type.Id }, response);
        }

        [HttpDelete("{id:int}")]
        public async override Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await typeService.DeleteType<OfferType>(id);

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

        [HttpGet("{id:int}")]
        public async override Task<IActionResult> GetById([FromRoute] int id)
        {
            var response = new TypeResponseModel();

            try
            {
                var offerType = await typeService.GetById<OfferType>(id);

                response.Status = StatusCodes.Status200OK;
                response.Type = mapper.Map<TypeModel>(offerType);
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
        public override async Task<IActionResult> GetAll()
        {
            var response = new AllTypesResponseModel();

            var types = await typeService.GetAll<OfferType>();

            response.Status = StatusCodes.Status200OK;
            response.Types = mapper.Map<IEnumerable<TypeModel>>(types);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async override Task<IActionResult> Update([FromRoute] int id, [FromBody] TypeInputModel model)
        {
            var response = await typeService.UpdateType<OfferType>(id, model);

            if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }

            return AcceptedAtAction(nameof(GetById), new { id = response.Type.Id }, response);
        }
    }
}
