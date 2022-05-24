using Application.Repositories;
using MediatR;

namespace Application.Commands.Product.Delete
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
    {
        private readonly IProductWriteRepository _productWriteRepository;

        public DeleteProductCommandHandler(IProductWriteRepository productWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.RemoveByIdAsync(request.Id);
            return await Task.FromResult(Unit.Value);
        }
    }
}
