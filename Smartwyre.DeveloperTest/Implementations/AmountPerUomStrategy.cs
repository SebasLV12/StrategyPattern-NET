using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;


namespace Smartwyre.DeveloperTest.Implementations
{
    public class AmountPerUomStrategy : ICalculateRebateStrategy
    {
        public decimal CalculateRebateAmount(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate.Amount * request.Volume;
        }

        public bool CanCalculate(Rebate rebate, Product product, CalculateRebateRequest request)
        {
            return rebate != null
                && product != null
                && product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom)
                && rebate.Amount != 0
                && request.Volume != 0;
        }   
    }
}
