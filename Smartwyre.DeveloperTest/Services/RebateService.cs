using Smartwyre.DeveloperTest.Interfaces;
using Smartwyre.DeveloperTest.Types;
﻿using System;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService

{
    private readonly IRebateDataStore rebateDataStore;
    private readonly IProductDataStore productDataStore;
    private readonly IRebateStrategyDictionary strategyDictionary;


    public RebateService(IRebateDataStore rebateDataStore, IProductDataStore productDataStore, IRebateStrategyDictionary strategyDictionary)
    {
        this.rebateDataStore = rebateDataStore ?? throw new ArgumentNullException(nameof(rebateDataStore));
        this.productDataStore = productDataStore ?? throw new ArgumentNullException(nameof(productDataStore));
        this.strategyDictionary = strategyDictionary ?? throw new ArgumentNullException(nameof(strategyDictionary));

    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = productDataStore.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();


        try
        {
            var strategy = strategyDictionary.GetStrategy(rebate.Incentive);

            if (strategy.CanCalculate(rebate, product, request))
            {
                var rebateAmount = strategy.CalculateRebateAmount(rebate, product, request);
                result.Success = true;

                rebateDataStore.StoreCalculationResult(rebate, rebateAmount);

            }
            else
            {
                result.Success = false;
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Error calculating rebate: {ex.Message}. Rebate ID: {rebate.Identifier}, Product ID: {product.Identifier}");
            result.Success = false; 
        }

        return result;
    }
}

