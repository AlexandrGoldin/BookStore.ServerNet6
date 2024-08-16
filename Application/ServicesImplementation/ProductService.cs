using Application.ApplicationDTOs;
using Application.ServicesInterfaces;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;

namespace Application.ServicesImplementation
{
    public class ProductService : IProductService
    {
        private readonly IDapperProductRepository _dapperProductRepo;
        private readonly IMapper _mapper;

        public ProductService(IDapperProductRepository dapperProductRepo, IMapper mapper)
        {
            _dapperProductRepo = dapperProductRepo;
            _mapper = mapper;
        }

        public async Task<ProductReadDto?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {

            if (id == 0)
            {
                return null;
            }

            var product= await _dapperProductRepo.GetByIdAsync(id, cancellationToken);

            if (product == null)
            {
                return null;
            }         
            return _mapper.Map<ProductReadDto>(product);
        }

       
    }
}
