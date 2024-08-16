using Application.ApplicationDTOs;
using Application.ApplicationMappingProfiles;
using Application.ServicesImplementation;
using AutoMapper;
using Domain.DapperRepositoriesInterfaces;
using Domain.Entities;
using Moq;
using Xunit;

namespace BookStoreUnitTests.ApplicationServices.ProductServiceTests
{
    public class ProductServiceTests
    {
        private readonly Mock<IDapperProductRepository> _mockDapperProductRepo;
        private readonly IMapper _mapper;
        
        public ProductServiceTests()
        {
            _mockDapperProductRepo = new();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<ProductProfile>();
            });
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetProductByIdAsync_ShouldReturnCorrectProduct()
        {
            // Arrange
            var testProduct = GetTestProduct();
            _mockDapperProductRepo.Setup(r => r.GetByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(testProduct);

            // Act
            var productService = new ProductService(_mockDapperProductRepo.Object, _mapper);

            var result = await productService.GetByIdAsync(1, CancellationToken.None);

            // Assert
            Assert.IsType<ProductReadDto>(result);
            Assert.True(testProduct.Id == result!.Id && testProduct.Title == result.Title &&
                testProduct.Author == result.Author && testProduct.Image == result.Image &&
                testProduct.Price == result.Price && testProduct.Genre == result.Genre &&
                testProduct.Rating == result.Rating &&
                testProduct.Description == result.Description);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(999)]
        public async Task GetProductByIdAsync_ProductIdIsZero_Or_OutOfRange_ShouldReturnNull
            (int value)
        { 
            // Arrange
            var testProduct = GetTestProduct();
            _mockDapperProductRepo.Setup(r => r.GetByIdAsync(1, CancellationToken.None))
                .ReturnsAsync(testProduct);

            // Act
            var productService = new ProductService(_mockDapperProductRepo.Object, _mapper);

            var result = await productService.GetByIdAsync(value, CancellationToken.None);

            // Assert
            Assert.True(result == null);           
        }

        private Product GetTestProduct()
        {
            var testProduct = new Product()
            {
                Id = 1,
                Title = "TestTitle_1",
                Author = "TestAuthor_1",
                Image = "TestImage_1",
                Price = 100.00m,
                Genre = "TesrGenre_1",
                Rating = 1,
                Description = "TestDescription_1"
            };
            return testProduct;
        }
    }
}
