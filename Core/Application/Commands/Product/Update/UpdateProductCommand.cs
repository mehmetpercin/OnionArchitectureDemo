using Application.Queries.Product;
using MediatR;

namespace Application.Commands.Product.Update
{
    public class UpdateProductCommand : IRequest<ProductResponse>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
