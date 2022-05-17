using Application.Mappers;
using Application.Queries.Product;
using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.Product.Update
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductResponse>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = ObjectMapper.Mapper.Map<Domain.Entities.Product>(request);
            var result = await _productRepository.UpdateAsync(product);
            return ObjectMapper.Mapper.Map<ProductResponse>(result);
        }
    }
}
