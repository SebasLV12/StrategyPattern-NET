using Smartwyre.DeveloperTest.Implementations;
using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;
using System;
using System.Collections.Generic;

namespace Smartwyre.DeveloperTest.Strategies
{

    public class RebateStrategyDictionary : IRebateStrategyDictionary
    {
        private readonly Dictionary<IncentiveType, ICalculateRebateStrategy> strategyDictionary;

        public RebateStrategyDictionary()
        {
            strategyDictionary = new Dictionary<IncentiveType, ICalculateRebateStrategy>
        {
            { IncentiveType.FixedCashAmount, new FixedCashAmountStrategy() },
            { IncentiveType.FixedRateRebate, new FixedRateRebateStrategy() },
            { IncentiveType.AmountPerUom, new AmountPerUomStrategy() }
        };
        }

        public ICalculateRebateStrategy GetStrategy(IncentiveType incentiveType)
        {
            if (strategyDictionary.TryGetValue(incentiveType, out var strategy))
            {
                return strategy;
            }

            throw new ArgumentException($"Strategy for the incentive type was not found {incentiveType}");
        }
    }

}