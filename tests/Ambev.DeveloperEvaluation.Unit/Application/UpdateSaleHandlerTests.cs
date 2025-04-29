using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class UpdateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly UpdateSaleHandler _handler;

        public UpdateSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new UpdateSaleHandler(_saleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsUpdateSaleResult()
        {
            // Arrange
            var command = new UpdateSaleCommand
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                SaleNumber = 1,
                SaleDate = DateTime.UtcNow,
                Branch = "Branch1",
                Items = [
                            new UpdateSaleItem { ProductId = Guid.NewGuid(), Quantity = 5, UnitPrice = 10, Discount = 0, TotalAmount = 50 }
                        ]
            };

            var sale = new Sale
            {
                Id = command.Id,
                CustomerId = command.CustomerId,
                SaleNumber = command.SaleNumber,
                SaleDate = command.SaleDate,
                Branch = command.Branch,
                Items = [
                            new SaleItem(command.Items.First().ProductId, command.Items.First().Quantity, command.Items.First().UnitPrice)
                        ]
            };

            // Simulate what happens after CalculateTotalSaleAmount
            sale.CalculateTotalSaleAmount();


            _saleRepositoryMock.Setup(repo => repo.GetByIdAsync(command.Id)).ReturnsAsync(sale);
            _mapperMock.Setup(m => m.Map(command, sale)).Returns(sale);
            _mapperMock.Setup(m => m.Map<UpdateSaleResult>(It.IsAny<Sale>()))
                       .Returns((Sale s) => new UpdateSaleResult { Id = s.Id, TotalSaleAmount = s.TotalSaleAmount });
            _saleRepositoryMock.Setup(repo => repo.UpdateAsync(sale)).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.Id);
            Assert.Equal(sale.TotalSaleAmount, result.TotalSaleAmount);
        }
    }
}
