using Application.Repositories;
using MediatR;

namespace Application.Commands.Product.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productRepository.RemoveByIdAsync(request.Id);
            return await Task.FromResult(Unit.Value);
        }
    }
}
