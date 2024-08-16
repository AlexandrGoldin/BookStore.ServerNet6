using Application.ApplicationDTOs;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ProductReadDto>
    {
        public int ProductId { get; set; }
    }
}
