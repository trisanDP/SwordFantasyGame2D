using System.Collections.Generic;
using UnityEngine;
using BrokenLands;

[CreateAssetMenu(fileName = "NewHubData", menuName = "Hub/New Hub")]
public class HubData : ScriptableObject {


    public int[] creditExchangeRates;  // Exchange rates for resources

    public List<ResourceType> resourceTypes;  // A list of all available resource types
    public Storage hubstorage = new Storage();

    public void Initialize() {
        int resourceCount = resourceTypes.Count;
/*        storedResources = new int[resourceCount];*/
        creditExchangeRates = new int[resourceCount];
    }



    // Set exchange rates for each resource
    public void SetCreditExchangeRate(ResourceType resource, int exchangeRate) {
        int index = resource.resourceIndex;

        if(index >= 0 && index < creditExchangeRates.Length) {
            creditExchangeRates[index] = exchangeRate;
        } else {
            Debug.LogError("Invalid resource index for " + resource.resourceName);
        }
    }

    // Get the credit exchange rate for a specific resource
    public int GetCreditExchangeRate(ResourceType resource) {
        int index = resource.resourceIndex;

        if(index >= 0 && index < creditExchangeRates.Length) {
            return creditExchangeRates[index];
        } else {
            Debug.LogError("Invalid resource index for " + resource.resourceName);
            return 0;
        }
    }

}
