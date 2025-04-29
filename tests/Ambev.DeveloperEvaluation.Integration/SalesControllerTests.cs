using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Integration.Helper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Tests
{
    public class SalesControllerTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;
        private readonly UserHelper _userHelper;
        
        public SalesControllerTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
            _userHelper = new UserHelper();
        }

        [Fact]
        public async Task CreateSale_WithInvalidToken_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "invalid-token");

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/sales", new CreateSaleCommand());

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


        [Fact]
        public async Task CreateSale_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var createSaleRequest = new CreateSaleCommand
            {
                SaleNumber = 1,
                CustomerId = Guid.NewGuid(),
                TotalSaleAmount = 100.00m,
                Branch = "Main Branch",
                Items =
                        [
                            new CreateSaleItem
                            {
                                ProductId = Guid.NewGuid(),
                                Quantity = 2,
                                UnitPrice = 50.00m,
                            }
                        ]
            };


            // Create user
            var userRequest = _userHelper.GenerateRandomCustomerRequest();
            var token  = await _userHelper.CreateRandomCustomerGetTokenFromAsync(client, userRequest);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsJsonAsync("/api/v1/sales", createSaleRequest);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Sales created successfully", responseString);
        }
    }
}
