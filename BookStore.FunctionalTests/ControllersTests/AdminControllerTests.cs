using Domain.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace BookStore.FunctionalTests.ControllersTests
{
    public class AdminControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public AdminControllerTests(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/1/admin")]
        [InlineData("/api/1/admin/index")]
        [InlineData("/api/2/admin")]
        [InlineData("/api/2/admin/index")]
        public async Task GetAllAsync_WhenTheCallIsRight_ShouldReturnCorrectModelList_200(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); 

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType?.ToString());
        }

        [Theory]
        [InlineData("/api/1/Admin/GetOrderAsync/2")]
        [InlineData("/api/1/Admin/1")]
        [InlineData("/api/2/Admin/GetOrderAsync/2")]
        [InlineData("/api/2/Admin/1")]
        public async Task GetAsync_WhenOrderIdIsRight_ShouldReturnSuccessModel_200(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); 

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType?.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/1/Admin/GetOrderAsync/999")]
        [InlineData("/api/1/Admin/999")]
        [InlineData("/api/2/Admin/GetOrderAsync/999")]
        [InlineData("/api/2/Admin/999")]
        public async Task GetAsync_WhenModelIdIsOutOfRange_ShouldReturnNotFound_404(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //[Fact]
        [Theory]
        [InlineData("/api/1/Admin/GetOrderAsync/0")]
        [InlineData("/api/2/Admin/GetOrderAsync/0")]
        public async Task GetOrderAsync_WhenOrderIdIsZero_ShouldReturnNotFound_400(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        // [Fact]
        [Theory]
        [InlineData("/api/1/Admin/0")]
        [InlineData("/api/2/Admin/0")]
        public async Task GetProductAsync_WhenOrderIdIsZero_ShouldReturnNotFound_404(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        //[Fact]
        [Theory]
        [InlineData("/api/1/Admin")]
        [InlineData("/api/2/Admin")]
        public async Task GetAllProductsAsync_WhenTheCallIsRight_ShouldReturnSuccessAllProducts_200
            (string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); 

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType?.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        //[Fact]
        [Theory]
        [InlineData("/api/1/admin/DeleteOrder/")]
        [InlineData("/api/2/admin/DeleteOrder/")]
        public async Task DeleteOrderAsync_WhenOrderIdIsRight_ShouldReturnSuccessNoContent_204
            (string url)
        {
            // Arrange
            var testOrder = GetTestOrder();

            // Act
            var postResponse = await _client.PostAsJsonAsync("/api/1/users", testOrder);

            var stringResult = await postResponse.Content.ReadAsStringAsync();

            Order orderResponse = JsonConvert.DeserializeObject<Order>(stringResult);

            var deleteResponse = await _client.DeleteAsync(url+$"{orderResponse.Id}");

            // Assert   
            postResponse.EnsureSuccessStatusCode(); 
            Assert.Equal("application/json; charset=utf-8",
                postResponse.Content.Headers.ContentType?.ToString());

            deleteResponse.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        private Order GetTestOrder()
        {
            var testOrder = new Order
            {
                Email = "Email.Test_1@mail.com",
                Name = "TestName_1",
                Address = "TestAddress 1",
                OrderDate = DateTime.Now,
                Total = 100.00m,
                CartItems = new List<CartItem>()
                    {
                     new CartItem() {ProductId = 3, Count = 2}
                    }
            };
            return testOrder;
        }
    }
}


