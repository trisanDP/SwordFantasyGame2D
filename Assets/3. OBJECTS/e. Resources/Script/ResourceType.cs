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

public interface IResourceEntity {
    void ProduceResources(Dictionary<ResourceType, int> resourcePackage);
    void ConsumeResources(Dictionary<ResourceType, int> resourcePackage);
    void ReceiveResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity from);
    void SendResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity to);

    string GetEntityName();
    Transform GetTransform();
}


