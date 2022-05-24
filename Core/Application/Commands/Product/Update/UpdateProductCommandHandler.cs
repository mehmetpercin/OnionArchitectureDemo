using Application.Mappers;
using Application.Queries.Product;
using Application.Repositories;
using MediatR;

namespace Application.Commands.Product.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public UpdateProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ObjectMapper.Mapper.Map<Domain.Entities.Product>(request);
            var result = await _productWriteRepository.UpdateAsync(product);
            return ObjectMapper.Mapper.Map<ProductResponse>(result);
        }
    }
}
