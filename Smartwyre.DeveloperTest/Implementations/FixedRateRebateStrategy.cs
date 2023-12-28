using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Implementations
{
    public class FixedRateRebateStrategy : ICalculateRebateStrategy
    {
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return product.Price * rebate.Percentage * request.Volume;
        }

        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null
                && product != null
                && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate)
                && rebate.Percentage != 0
                && product.Price != 0
                && request.Volume != 0;
        }
    }
}
