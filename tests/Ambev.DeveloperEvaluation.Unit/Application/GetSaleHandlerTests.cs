using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class GetSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly GetSaleHandler _handler;

        public GetSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetSaleHandler(_saleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsGetSaleResult()
        {
            // Arrange
            var command = new GetSaleCommand
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };

            var sale = new Sale
            {
                Id = command.Id,
                CustomerId = command.CustomerId,
                SaleNumber = 1,
                SaleDate = DateTime.UtcNow,
                Branch = "Branch1",
                Items = new List<SaleItem>
                {
                    new SaleItem(Guid.NewGuid(), 5, 10)
                }
            };

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(sale);
            _mapperMock.Setup(m => m.Map<GetSaleResult>(sale)).Returns(new GetSaleResult { Id = sale.Id, TotalSaleAmount = sale.TotalSaleAmount });

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.Id);
            Assert.Equal(sale.TotalSaleAmount, result.TotalSaleAmount);
        }

        [Fact]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = new GetSaleCommand
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync((Sale)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_UnauthorizedCustomer_ThrowsUnauthorizedAccessException()
        {
            // Arrange
            var command = new GetSaleCommand
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid()
            };

            var sale = new Sale
            {
                Id = command.Id,
                CustomerId = Guid.NewGuid(), // Different CustomerId
                SaleNumber = 1,
                SaleDate = DateTime.UtcNow,
                Branch = "Branch1",
                Items = new List<SaleItem>
                {
                    new SaleItem(Guid.NewGuid(), 5, 10)
                }
            };

            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(sale);

            // Act & Assert
            await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
