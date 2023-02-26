using AutoMapper;
using bgbrokersapi.Models.InputModels;
using bgbrokersapi.Services.Admin.Type;
using Microsoft.AspNetCore.Mvc;

namespace bgbrokersapi.Controllers.Admin
{
    [Route("/Admin/Type")]
    [ApiController]
    public abstract class TypeController : ControllerBase
    {
        protected readonly ITypeService typeService;
        protected readonly IMapper mapper;

        protected TypeController(ITypeService typeService,
             IMapper mapper)
        {
            this.typeService = typeService;
            this.mapper = mapper;
        }

        public abstract Task<IActionResult> Create(TypeInputModel model);
        public abstract Task<IActionResult> GetById([FromRoute] int id);
        public abstract Task<IActionResult> Delete([FromRoute] int id);
        public abstract Task<IActionResult> Update([FromRoute] int id, [FromBody] TypeInputModel model);
        public abstract Task<IActionResult> GetAll();

    }
}
