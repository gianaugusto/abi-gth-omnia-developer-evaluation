using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application
{
    public class CreateSaleHandlerTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CreateSaleHandler _handler;

        public CreateSaleHandlerTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _mapperMock = new Mock<IMapper>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new CreateSaleHandler(_saleRepositoryMock.Object, _mapperMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_ValidCommand_ReturnsCreateSaleResult()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var customerId = Guid.NewGuid();

            var command = new CreateSaleCommand
            {
                CustomerId = customerId,
                SaleNumber = 1,
                Branch = "Branch1",
                Items = [
                            new CreateSaleItem
                            {
                                ProductId = productId,
                                Quantity = 5, // triggers 10% discount
                                UnitPrice = 10
                            }
                        ]
            };

            // Create SaleItem using actual constructor logic
            var saleItem = new SaleItem(productId, 5, 10); // will apply 10% discount
            var totalAmount = saleItem.TotalAmount;

            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                SaleNumber = command.SaleNumber,
                Branch = command.Branch,
                Items = new List<SaleItem> { saleItem },
                TotalSaleAmount = totalAmount
            };

            _mapperMock.Setup(m => m.Map<Sale>(command)).Returns(sale);
            _mapperMock.Setup(m => m.Map<CreateSaleResult>(sale)).Returns(new CreateSaleResult
            {
                SaleId = sale.Id,
                TotalAmount = totalAmount
            });

            _saleRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Sale>())).Returns(Task.CompletedTask);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(sale.Id, result.SaleId);
            Assert.Equal(totalAmount, result.TotalAmount);
        }
    }
}
