using Application.Dtos;
using MediatR;

namespace Application.Commands.Product.Create
{
    public class CreateProductCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
