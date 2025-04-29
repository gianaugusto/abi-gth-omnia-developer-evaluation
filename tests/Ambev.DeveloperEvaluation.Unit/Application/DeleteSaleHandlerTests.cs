using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class DeleteSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<ISaleEventProducer> _saleEventProducerMock;

        private readonly DeleteSaleHandler _handler;

        public DeleteSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _saleEventProducerMock = new Mock<ISaleEventProducer>();

            _handler = new DeleteSaleHandler(_saleRepositoryMock.Object, _saleEventProducerMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsDeleteSaleResponse()
        {
            // Arrange
            var command = new DeleteSaleCommand
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
            _saleRepositoryMock.Setup(repo => repo.DeleteAsync(command.Id)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task Handle_SaleNotFound_ThrowsKeyNotFoundException()
        {
            // Arrange
            var command = new DeleteSaleCommand
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
            var command = new DeleteSaleCommand
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
