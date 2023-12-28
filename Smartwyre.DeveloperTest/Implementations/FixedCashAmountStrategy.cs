using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Implementations
{
    public class FixedCashAmountStrategy : ICalculateRebateStrategy
    {
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount;           
        }

        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null
               && product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount)
               && rebate.Amount != 0;
        }
    }
}
