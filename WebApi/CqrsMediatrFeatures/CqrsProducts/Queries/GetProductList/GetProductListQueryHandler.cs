using Application.ApplicationDTOs;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using MediatR;

namespace WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductList
{
    public class GetProductListQueryHandler  
        : IRequestHandler<GetProductListQuery, ProductListReadDto>
    {
        private readonly IDapperProductRepository _dapperProductRepo;
        private readonly IMapper _mapper;

        public GetProductListQueryHandler(IMapper mapper,
            IDapperProductRepository dapperProductRepo)
        {
            _dapperProductRepo = dapperProductRepo;
            _mapper = mapper;
        }
        public async Task<ProductListReadDto> Handle(GetProductListQuery request,
            CancellationToken cancellationToken)
        {
            await Task.Delay(1500);

            var productListReadDto =_mapper.Map<List<ProductReadDto>>(await _dapperProductRepo
                .GetAllAsync(cancellationToken)).ToList();

            return new ProductListReadDto { Products = productListReadDto };
        }
    }
}
