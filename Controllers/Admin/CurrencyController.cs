using AutoMapper;
using bgbrokersapi.Data.Models.Types;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Admin.Type;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bgbrokersapi.Controllers.Admin
{
    [Route("/Currency")]
    [ApiController]
    public class CurrencyController : TypeController
    {
        public CurrencyController(ITypeService typeService,
             IMapper mapper)
            : base(typeService, mapper)
        {
        }

        [HttpPost]
        public async override Task<IActionResult> Create(TypeInputModel model)
        {
            var response = await typeService.Create<Currency>(model);

            if (response.Status == StatusCodes.Status400BadRequest)
            {
                return BadRequest(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Type.Id }, response);
        }

        [HttpDelete("{id:int}")]
        public async override Task<IActionResult> Delete([FromRoute] int id)
        {
            var response = await typeService.DeleteType<Currency>(id);

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
                var currency = await typeService.GetById<Currency>(id);

                response.Status = StatusCodes.Status200OK;
                response.Type = mapper.Map<TypeModel>(currency);
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

            var types = await typeService.GetAll<Currency>();

            response.Status = StatusCodes.Status200OK;
            response.Types = mapper.Map<IEnumerable<TypeModel>>(types);

            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async override Task<IActionResult> Update([FromRoute] int id, [FromBody] TypeInputModel model)
        {
            var response = await typeService.UpdateType<Currency>(id, model);

            if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }

            return AcceptedAtAction(nameof(GetById), new { id = response.Type.Id }, response);
        }
    }
}
