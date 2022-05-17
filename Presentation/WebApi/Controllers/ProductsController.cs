using Application.Commands.Product.Create;
using Application.Commands.Product.Delete;
using Application.Commands.Product.Update;
using Application.Queries.Product;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsQuery request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody]CreateProductCommand request)
        {        
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteProductCommand request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProductCommand request)
        {
            return Ok(await _mediator.Send(request));
        }
    }
}
