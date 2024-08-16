using Application.ApplicationDTOs;
using Application.ApplicationMappingProfiles;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Moq;
using WebApi.CqrsMediatrFeatures.CqrsProducts.Queries.GetProductById;
using Xunit;

namespace BookStoreUnitTests.Products.Queries
{
    public class GetProductByIdQueryHandlerTests
    {
        private readonly Mock<IDapperProductRepository> _mockDapperProductRepo;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandlerTests()
        {
            _mockDapperProductRepo = new();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetProductQueryHandlerAsync_ShouldReturnCorrectProduct()
        {
            // Arrange
            var testProduct = GetTestProduct();
            _mockDapperProductRepo.Setup(r => r.GetByIdAsync(1, CancellationToken.None))
               .ReturnsAsync(testProduct);

            var handler = new GetProductByIdQueryHandler(_mockDapperProductRepo.Object, _mapper);

            // Act
            var result = await handler.Handle(
                new GetProductByIdQuery
                {
                    ProductId = 1,
                },
                 CancellationToken.None);

            // Assert
            Assert.IsType<ProductReadDto>(result);
            Assert.Equal(testProduct.Title, result!.Title);           
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetProductQueryHandlerAsync_ProductIdIsOutOfRange_Or_Zero_ShouldReturnNull
            (int value)
        {
            // Arrange
            var testProduct = GetTestProduct();
            _mockDapperProductRepo.Setup(r => r.GetByIdAsync(1,CancellationToken.None))
               .ReturnsAsync(testProduct);

            var handler = new GetProductByIdQueryHandler(_mockDapperProductRepo.Object, _mapper);

            // Act
            var result = await handler.Handle(
                new GetProductByIdQuery
                {
                    ProductId = value,
                },
                 CancellationToken.None);

            // Assert
            Assert.True(result == null);
        }

        private Product GetTestProduct()
        {
            var testProduct = new Product
            {
                Id = 1, Title = "Title_1", Author = "Author_1", Image = "Image_1",
                Price = 100.00m, Genre = "Genre_1", Rating = 1, Description = "Description_1."
            };
            return testProduct;
        }
    }
}
