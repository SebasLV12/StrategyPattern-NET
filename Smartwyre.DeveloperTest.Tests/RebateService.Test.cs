using System;
using Xunit;
using Moq;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Implementations;


namespace Smartwyre.DeveloperTest.Tests
{
    public class RebateServiceTest
    {
        private Mock<IRebateDataStore> rebateDataStoreMock;
        private Mock<IProductDataStore> productDataStoreMock;
        private Mock<IRebateStrategyDictionary> strategyDictionaryMock;
        private RebateService rebateService;
        private CalculateRebateRequest request;
        private Rebate rebate;
        private Product product;

        public RebateServiceTest()
        {
            rebateDataStoreMock = new Mock<IRebateDataStore>();
            productDataStoreMock = new Mock<IProductDataStore>();
            strategyDictionaryMock = new Mock<IRebateStrategyDictionary>();
            rebateService = new RebateService(rebateDataStoreMock.Object, productDataStoreMock.Object, strategyDictionaryMock.Object);


        }
        
        [Fact]
        public void Calculate_WithValidFixedCashAmountIncentive_SuccessfullyCalculatesRebate()
        {
            // Arrange
            SetupMocksForFixedCashAmountIncentive();

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);
        }

        [Fact]
        public void Calculate_WithInvalidFixedCashAmountIncentive_ReturnsFailure()
        {
            // Arrange
            SetupMocksForInvalidFixedCashAmountIncentive();

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);

       
        }

        [Fact]
        public void Calculate_WithValidFixedRateRebateIncentive_SuccessfullyCalculatesRebate()
        {
            // Arrange

            SetupMocksForFixedRateRebateIncentive();

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            Assert.True(result.Success);


        }

        [Fact]
        public void Calculate_WithInvalidFixedRateRebateIncentive_ReturnsFailure()
        {
            // Arrange
            SetupMocksForInvalidFixedRateRebateIncentive();

            // Act
            var result = rebateService.Calculate(request);

            // Assert
            Assert.False(result.Success);
        }

        private void SetupMocksForFixedCashAmountIncentive()
        {
            request = new CalculateRebateRequest { RebateIdentifier = "", ProductIdentifier = "", Volume = 100 };
            rebate = new Rebate { Amount = 100, Identifier = "0", Incentive = IncentiveType.FixedCashAmount, Percentage = 100 };
            product = new Product { Id = 0, Identifier = "0", Price = 100, SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Uom = "0" };

            rebateDataStoreMock.Setup(d => d.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStoreMock.Setup(d => d.GetProduct(It.IsAny<string>())).Returns(product);


            strategyDictionaryMock.Setup(d => d.GetStrategy(IncentiveType.FixedCashAmount)).Returns(new FixedCashAmountStrategy());
        }

        private void SetupMocksForInvalidFixedCashAmountIncentive()
        {
            request = new CalculateRebateRequest { RebateIdentifier = "", ProductIdentifier = "", Volume = 100 };
            rebate = new Rebate { Amount = 100, Identifier = "0", Incentive = IncentiveType.FixedCashAmount, Percentage = 100 };
            product = new Product { Id = 0, Identifier = "0", Price = 100, SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Uom = "0" };

            rebateDataStoreMock.Setup(d => d.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStoreMock.Setup(d => d.GetProduct(It.IsAny<string>())).Returns(product);


            strategyDictionaryMock.Setup(d => d.GetStrategy(It.IsAny<IncentiveType>())).Returns(new FixedCashAmountStrategy());
        }
        private void SetupMocksForFixedRateRebateIncentive()
        {

            request = new CalculateRebateRequest { RebateIdentifier = "0", ProductIdentifier = "0", Volume = 100 };
            rebate = new Rebate { Amount = 100, Identifier = "0", Incentive = IncentiveType.FixedRateRebate, Percentage = 100 };
            product = new Product { Id = 0, Identifier = "0", Price = 100, SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Uom = "0" };

            rebateDataStoreMock.Setup(d => d.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStoreMock.Setup(d => d.GetProduct(It.IsAny<string>())).Returns(product);

            strategyDictionaryMock.Setup(d => d.GetStrategy(IncentiveType.FixedRateRebate)).Returns(new FixedRateRebateStrategy());

        }

        private void SetupMocksForInvalidFixedRateRebateIncentive()
        {
            request = new CalculateRebateRequest { RebateIdentifier = "", ProductIdentifier = "", Volume = 100 };
            rebate = new Rebate { Amount = 100, Identifier = "0", Incentive = IncentiveType.FixedCashAmount, Percentage = 100 };
            product = new Product { Id = 0, Identifier = "0", Price = 100, SupportedIncentives = SupportedIncentiveType.FixedCashAmount, Uom = "0" };

            rebateDataStoreMock.Setup(d => d.GetRebate(It.IsAny<string>())).Returns(rebate);
            productDataStoreMock.Setup(d => d.GetProduct(It.IsAny<string>())).Returns(product);


            strategyDictionaryMock.Setup(d => d.GetStrategy(IncentiveType.FixedRateRebate)).Returns(new FixedRateRebateStrategy());
        }
    }
}

