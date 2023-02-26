using AutoMapper;
using bgbrokersapi.Models;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Offers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bgbrokersapi.Controllers
{

    [ApiController]
    [Route("/Offer")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService offerService;
        private readonly IMapper mapper;

        public OfferController(IOfferService offerService,
            IMapper mapper)
        {
            this.offerService = offerService;
            this.mapper = mapper;
        }

        [Authorize(Roles = $"{Constants.RoleConstants.AdminRole}, {Constants.RoleConstants.BrokerRole}")]
        [HttpPost]
        public async Task<IActionResult> Create(OfferInputModel model)
        {
            var response = await offerService.CreateOffer(model, User.FindFirst(ClaimTypes.NameIdentifier));

            if (response.Status == 400)
            {
                return BadRequest(response);
            }

            if (response.Status == 404)
            {
                return NotFound(response);
            }

            return CreatedAtAction(nameof(GetById), new { id = response.Offer.Id }, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = new AllOffersResponseModel();

            var offers = await offerService.GetAllOffers();

            response.Status = StatusCodes.Status200OK;
            response.Offers = mapper.Map<IEnumerable<OfferModel>>(offers);

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var response = new OfferResponseModel();

            try
            {
                var offer = await offerService.GetById(id);

                response.Status = StatusCodes.Status200OK;
                response.Offer = mapper.Map<OfferModel>(offer);

            }
            catch (Exception e)
            {
                response.Status = StatusCodes.Status404NotFound;
                response.Message = e.Message;

                return NotFound(response);
            }

            return Ok(response);
        }

        [Authorize(Roles = $"{Constants.RoleConstants.AdminRole}, {Constants.RoleConstants.BrokerRole}")]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, OfferInputModel model)
        {
            var response = await offerService.UpdateOffer(id, model, User.FindFirst(ClaimTypes.NameIdentifier));

            if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }

            return AcceptedAtAction(nameof(GetById), new { id = id }, response);
        }

        [Authorize(Roles = Constants.RoleConstants.AdminRole)]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await offerService.DelteFromDb(id);

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

        [Authorize(Roles = $"{Constants.RoleConstants.AdminRole}, {Constants.RoleConstants.BrokerRole}")]
        [HttpPut("/CasualDelete/{id:guid}")]
        public async Task<IActionResult> CasualDelete([FromRoute] Guid id)
        {
            var response = await offerService.CasualDelete(id);

            if (response.Status == StatusCodes.Status404NotFound)
            {
                return NotFound(response);
            }

            return Accepted(response);
        }
    }
}
