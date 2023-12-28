using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Interfaces
{
    public interface ICalculateRebateStrategy
    {
        bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request);
        decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request);
    }
}
