using Domain.Common;
using MediatR;

namespace Application.Queries.Product
{
    public class GetAllProductsQuery : IRequest<PageableResponse<List<ProductResponse>>>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
