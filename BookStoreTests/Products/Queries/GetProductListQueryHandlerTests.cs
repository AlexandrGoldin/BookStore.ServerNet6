using Application.ApplicationMappingProfiles;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductList;
using Xunit;

namespace BookStoreUnitTests.Products.Queries
{
    public class GetProductListQueryHandlerTests
    {
        private readonly Mock<IDapperProductRepository> _mockDapperProductRepo;
        private readonly ILogger<GetProductListQueryHandler> _logger;
        private readonly IMapper _mapper;

        public GetProductListQueryHandlerTests()
        {
            _mockDapperProductRepo = new();

            using var logFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = logFactory.CreateLogger<GetProductListQueryHandler>();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetProductListQueryHandlerAsync_ShouldReturnAllProducts()
        {
            // Arrange
            var testProducts = GetTestProducts();
            _mockDapperProductRepo.Setup(r => r.GetAllAsync(CancellationToken.None))
               .ReturnsAsync(testProducts);

            var handler = new GetProductListQueryHandler(_mapper, 
                _mockDapperProductRepo.Object);

            // Act
           var result = await handler.Handle(new GetProductListQuery(),
                CancellationToken.None);

            // Assert
            Assert.IsType<ProductListReadDto>(result);
            Assert.Equal(testProducts.Count, result.Products?.Count);
        }

        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>();
            testProducts.Add(new Product
            { 
                Id = 1, Title = "Title_1", Author = "Author_1", Image = "Image_1",
                Price = 100.00m, Genre = "Genre_1", Rating = 1, Description = "Description_1."
            });
            testProducts.Add(new Product
            {
                Id = 2, Title = "Title_2", Author = "Author_2", Image = "Image_2",
                Price = 200.00m, Genre = "Genre_2", Rating = 2, Description = "Description_2."
            });

            return testProducts;
        }
    }
}
