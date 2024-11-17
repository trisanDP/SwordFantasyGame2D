using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ResourceDatabase", menuName = "Resources/ResourceDatabase", order = 1)]
public class ResourceDatabase : ScriptableObject {
    public List<ResourceType> allResources;

    // Optional: Method to get a resource by name or index
    public ResourceType GetResourceByName(string resourceName) {
        return allResources.Find(resource => resource.resourceName == resourceName);
    }

    public ResourceType GetResourceByIndex(int index) {
        return allResources.Find(resource => resource.resourceIndex == index);
    }
}

