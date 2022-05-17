using MediatR;

namespace Application.Commands.Product.Delete
{
    public class DeleteProductCommand: IRequest
    {
        public string Id { get; set; }
    }
}
