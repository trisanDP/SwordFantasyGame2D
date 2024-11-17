using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewResourceType", menuName = "Resources/New Resource Type")]
public class ResourceType : ScriptableObject {
    public string resourceName;
    public int resourceIndex;
    public int creditValue;

    public int maxStackSize = 100; // Example: Limit on how many can be stacked
    public string resourceDescription; // Useful for tooltips or lore
    public Sprite resourceIcon; // For inventory or UI display
}

[System.Serializable]
public class Storage {
    // Dictionary to map resources to their amounts
    public Dictionary<ResourceType, int> storedResources = new Dictionary<ResourceType, int>();
    public int credits; // Currency

    // Add resources to storage
    public void AddResources(Dictionary<ResourceType, int> resourcePackage) {
        foreach(var entry in resourcePackage) {
            ResourceType resource = entry.Key;
            int amount = entry.Value;

            // If the resource is not already in storage, add it
            if(!storedResources.ContainsKey(resource)) {
                storedResources[resource] = 0;
            }

            // Update the resource amount
            storedResources[resource] += amount;
        }
    }

    // Remove resources from storage
    public void RemoveResource(ResourceType resource, int amount) {
        if(!storedResources.ContainsKey(resource)) {
            Debug.LogWarning($"Resource '{resource.resourceName}' not found in storage!");
            return;
        }

        // Reduce the resource amount
        storedResources[resource] = Mathf.Max(storedResources[resource] - amount, 0);

        // Remove resource if the amount is zero
        if(storedResources[resource] == 0) {
            storedResources.Remove(resource);
        }

        // Update UI
        UiManager.Instance.npcUiManager.UpdateStorageUI();
    }

    // Get the amount of a specific resource
    public int GetResourceAmount(ResourceType resource) {
/*        Debug.Log(storedResources[resource]);*/
        if(!storedResources.ContainsKey(resource)) {
            Debug.LogWarning($"Resource '{resource.resourceName}' not found in storage!");
            return 0;
        }

        return storedResources[resource];
    }

    // Find a resource by name
    public ResourceType GetResourceByName(string resourceName) {
        foreach(var resource in storedResources.Keys) {
            if(resource.resourceName.Equals(resourceName, System.StringComparison.OrdinalIgnoreCase)) {
                return resource;
            }
        }

        Debug.LogWarning($"Resource with name '{resourceName}' not found.");
        return null;
    }
}


public interface IResourceEntity {
    void ProduceResources(Dictionary<ResourceType, int> resourcePackage);
    void ConsumeResources(Dictionary<ResourceType, int> resourcePackage);
    void ReceiveResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity from);
    void SendResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity to);

    string GetEntityName();
    Transform GetTransform();
}


