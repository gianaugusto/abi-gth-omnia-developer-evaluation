using System.Net.Http.Json;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Tests
{
    public class SalesControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public SalesControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<DefaultContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<DefaultContext>(options =>
                    {
                        options.UseInMemoryDatabase("AmbevIntegrationTests");
                    });
                });
            });
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
                                Discount = 0.00m,
                                TotalAmount = 100.00m
                            }
                        ]
            };

            var response = await client.PostAsJsonAsync("/api/v1/sales", createSaleRequest);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Sales created successfully", responseString);
        }

        [Fact]
        public async Task GetSale_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var saleId = Guid.NewGuid();

            var response = await client.GetAsync($"/api/v1/sales/{saleId}");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Sale retrieved successfully", responseString);
        }

        [Fact]
        public async Task UpdateSale_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var saleId = Guid.NewGuid();
            var updateSaleRequest = new UpdateSaleCommand
            {
                Id = Guid.NewGuid(),
                SaleNumber = 1,
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                Branch = "Main Branch",
                Items =
                        [
                            new UpdateSaleItem
                            {
                                ProductId = Guid.NewGuid(),
                                Quantity = 2,
                                UnitPrice = 50.00m,
                                Discount = 0.00m,
                                TotalAmount = 100.00m
                            }
                        ]
            };

            var response = await client.PutAsJsonAsync($"/api/v1/sales/{saleId}", updateSaleRequest);

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Sale updated successfully", responseString);
        }

        [Fact]
        public async Task DeleteSale_ReturnsSuccessStatusCode()
        {
            var client = _factory.CreateClient();

            var saleId = Guid.NewGuid();

            var response = await client.DeleteAsync($"/api/v1/sales/{saleId}");

            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            Assert.Contains("Sale deleted successfully", responseString);
        }
    }
}
