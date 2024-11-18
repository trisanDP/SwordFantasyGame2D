using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class StoredResource {
    public ResourceType resourceType; // The type of resource
    public int amount;                // The amount of this resource

    public StoredResource(ResourceType resourceType, int amount) {
        this.resourceType = resourceType;
        this.amount = amount;
    }
}

[System.Serializable]
public class Storage {
    public List<StoredResource> storedResources = new List<StoredResource>();  // List of resources in storage
    public int credits;

    // Initialize storage with all resources from the ResourceDatabase
    public void InitializeStorage(ResourceDatabase resourceDatabase, int defaultAmount = 0) {
        if(resourceDatabase == null || resourceDatabase.allResources == null) {
            Debug.LogError("ResourceDatabase is null or empty. Cannot initialize storage.");
            return;
        }

        // Clear existing resources if necessary (e.g., if called more than once)
        storedResources.Clear();

        // Add each resource from the database to the storage with a default amount
        foreach(var resource in resourceDatabase.allResources) {
            storedResources.Add(new StoredResource(resource, defaultAmount));
        }

        Debug.Log($"Storage initialized with {storedResources.Count} resources.");
    }

    // Add resources to storage
    public void AddResources(Dictionary<ResourceType, int> resourcePackage) {
        foreach(var entry in resourcePackage) {
            ResourceType resource = entry.Key;
            int amount = entry.Value;

            // Find if the resource is already in storage
            var existingResource = storedResources.Find(r => r.resourceType == resource);

            if(existingResource != null) {
                // If the resource exists, update the amount
                existingResource.amount += amount;
            } else {
                // If the resource doesn't exist, add a new entry
                storedResources.Add(new StoredResource(resource, amount));
            }
        }
    }

    // Remove resources from storage
    public void RemoveResource(ResourceType resource, int amount) {
        var existingResource = storedResources.Find(r => r.resourceType == resource);

        if(existingResource != null) {
            existingResource.amount = Mathf.Max(existingResource.amount - amount, 0);

            // Remove resource if its amount becomes 0
            if(existingResource.amount == 0) {
                storedResources.Remove(existingResource);
            }
        } else {
            Debug.LogWarning($"Resource '{resource.resourceName}' not found in storage!");
        }
    }

    // Get the amount of a specific resource
    public int GetResourceAmount(ResourceType resource) {
        var existingResource = storedResources.Find(r => r.resourceType == resource);
        return existingResource != null ? existingResource.amount : 0;
    }
}




