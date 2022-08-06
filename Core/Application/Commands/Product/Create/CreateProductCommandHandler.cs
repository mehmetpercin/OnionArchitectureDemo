using Application.Dtos;
using Application.Repositories;
using Domain.Events;
using MassTransit;
using MediatR;

namespace Application.Commands.Product.Create
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Dtos.Response<string>>
    {
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository, IPublishEndpoint publishEndpoint)
        {
            _productWriteRepository = productWriteRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<Dtos.Response<string>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Domain.Entities.Product
            {
                Name = request.Name,
                Price = request.Price,
                Stock = request.Stock
            };
            await _productWriteRepository.AddAsync(product);
            var productCreatedEvent = new ProductCreatedEvent
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };

            await _publishEndpoint.Publish(productCreatedEvent, cancellationToken);
            return SuccessResponse<string>.Success(product.Id, 200);
        }
    }
}
