using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<ProductListReadDto>
    {
    }   
}
