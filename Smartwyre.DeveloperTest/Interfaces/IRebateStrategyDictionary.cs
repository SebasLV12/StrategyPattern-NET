using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Interfaces
{
    public interface IRebateStrategyDictionary
    {
        ICalculateRebateStrategy GetStrategy(IncentiveType incentiveType);
    }
}
