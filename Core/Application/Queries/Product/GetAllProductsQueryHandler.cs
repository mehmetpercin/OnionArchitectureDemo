using Application.Mappers;
using Application.Repositories;
using Domain.Common;
using MediatR;

namespace Application.Queries.Product
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, PageableResponse<List<ProductResponse>>>
    {
        private readonly IProductReadRepository _productRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<PageableResponse<List<ProductResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var data = _productRepository.GetAll().Skip(request.PageSize * (request.PageNumber - 1)).Take(request.PageSize).ToList();
            var result = new PageableResponse<List<ProductResponse>>
            {
                Data = ObjectMapper.Mapper.Map<List<ProductResponse>>(data),
                TotalCount = _productRepository.GetAll().Count()
            };
            return await Task.FromResult(result);
        }
    }
}
