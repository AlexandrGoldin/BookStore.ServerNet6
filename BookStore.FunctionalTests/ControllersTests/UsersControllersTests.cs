using Domain.Entities;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace BookStore.FunctionalTests.ControllersTests
{
    public class UsersControllerTests : IClassFixture<TestingWebAppFactory<Program>>
    {
        private readonly HttpClient _client;
        public UsersControllerTests(TestingWebAppFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("api/1/users")]
        [InlineData("api/2/users")]
        public async Task Index_WhenCalled_ShouldReturnSuccessEndCorrectProductList_200(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync("/api/1/users");

            // Assert
            response.EnsureSuccessStatusCode(); 

            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType?.ToString());
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);


        }

        [Theory]
        [InlineData("/api/1/Users/GetOrderAsync/2")]
        [InlineData("/api/2/Users/GetOrderAsync/2")]
        public async Task GetOrderAsync_WhenOrderIdIsRight_ShouldReturnSuccessOrder_200(string url)
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
        [InlineData("/api/1/Users/GetOrderAsync/1")]
        [InlineData("/api/2/Users/GetOrderAsync/1")]
        public async Task GetOrderAsync_WhenOrderIdIsOutOfRange_ShouldReturnNotFound_404(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);           
        }

        [Theory]
        [InlineData("/api/1/Users/GetOrderAsync/0")]
        [InlineData("/api/2/Users/GetOrderAsync/0")]
        public async Task GetOrderAsync_WhenOrderIdIsZero_ShouldReturnNotFound_400(string url)
        {
            // Arrange
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/1/users")]
        [InlineData("/api/2/users")]
        public async Task CreateOrderAsync_WhenRequestIsRight_ShouldReturnSuccessOrder_201(string url)
        {
            // Arrange
            var testOrder = GetTestOrder();

            // Act
            var response = await _client.PostAsJsonAsync(url, testOrder);

            var stringResult = await response.Content.ReadAsStringAsync();

            Order orderResponse = JsonConvert.DeserializeObject<Order>(stringResult);

            var testOrderDateString = testOrder.OrderDate.ToString("MM.yyyy");
            var orderResponseOrderDateString = orderResponse.OrderDate.ToString("MM.yyyy");

            // Assert   
            response.EnsureSuccessStatusCode(); 
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType?.ToString());
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            Assert.True(testOrder.Email == orderResponse.Email &&
                testOrder.Name == orderResponse.Name && 
                testOrder.Address == orderResponse.Address &&
                testOrderDateString == orderResponseOrderDateString &&
                testOrder.Total == orderResponse.Total);  
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
