using Application.ApplicationDTOs;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductById
{
    public class GetProductByIdQueryHandler 
        : IRequestHandler<GetProductByIdQuery, ProductReadDto?>
    {
        private readonly IDapperProductRepository _dapperProductRepo;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IDapperProductRepository dapperProductRepo, IMapper mapper)
        {
            _dapperProductRepo = dapperProductRepo;
            _mapper = mapper;
        }
       
        public async Task<ProductReadDto?> Handle(GetProductByIdQuery request, 
            CancellationToken cancellationToken)
        {
            var productReadDto = _mapper.Map<ProductReadDto>(await _dapperProductRepo
                .GetByIdAsync(request.ProductId, cancellationToken));

            if(productReadDto == null)
            {
                return null;
            }
            return productReadDto;
        }
    }
}
