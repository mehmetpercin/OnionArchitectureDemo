using Application.Repositories;
using MediatR;

namespace Application.Commands.Product.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock               
            };
            await _productRepository.AddAsync(product);
            return product.Id;
        }
    }
}
