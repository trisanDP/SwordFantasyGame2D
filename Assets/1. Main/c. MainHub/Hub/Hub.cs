using System.Collections.Generic;
using UnityEngine;

public class Hub : MonoBehaviour, IResourceEntity {

    public HubData hubData;
    DeliveryManager deliveryManager;

    #region Unity Lifecycle Methods

    private void Start() {
        // Load the HubData from resources and initialize
        hubData = Resources.Load<HubData>("HubDatabase");
        deliveryManager = DeliveryManager.instance;
        hubData.Initialize();
    }

    #endregion

    #region Resource Management Methods

    // Produces and stores resources in the hub
    public void ProduceResources(Dictionary<ResourceType, int> resourcePackage) {
        hubData.hubstorage.AddResources(resourcePackage);
        UiManager.Instance.hubUiManager.UpdateStorageUI(); // Update UI after production
    }

    // Consumes resources from the hub
    public void ConsumeResources(Dictionary<ResourceType, int> resourcePackage) {
        foreach(var entry in resourcePackage) {
            ResourceType resource = entry.Key;
            int amount = entry.Value;

            if(hubData.hubstorage.GetResourceAmount(resource) >= amount) {
                hubData.hubstorage.RemoveResource(resource, amount);
            } else {
                Debug.Log($"Not enough {resource.resourceName} in the hub to consume.");
            }
        }

        UiManager.Instance.hubUiManager.UpdateStorageUI(); // Update UI after consumption
    }

    // Receive resources and add them to the hub's storage
    public void ReceiveResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity from) {
        hubData.hubstorage.AddResources(resourcePackage);
        UiManager.Instance.hubUiManager.UpdateStorageUI(); // Update UI after receiving resources
    }

    // Processes the actual sending of resources
    public void SendResources(Dictionary<ResourceType, int> resourcePackage, IResourceEntity to) {
        Dictionary<ResourceType, int> deliverableResources = new();

        foreach(var entry in resourcePackage) {
            ResourceType resource = entry.Key;
            int amount = entry.Value;

            // Check if enough resources are available in the hub's storage
            if(hubData.hubstorage.GetResourceAmount(resource) >= amount) {
                deliverableResources.Add(resource, amount);
            } else {
                /*Debug.Log($"Not enough {resource} to send to {to.GetEntityName()}. Required: {amount}");*/
            }
        }

        if(deliverableResources.Count > 0) {
            deliveryManager.DeliverResources(this, to, deliverableResources, transform.position, to.GetTransform().position);

            // Remove the delivered resources from the hub's storage
            foreach(var entry in deliverableResources) {
                hubData.hubstorage.RemoveResource(entry.Key, entry.Value);
            }

            // Check delivery quest objectives using QuestFlowManager
/*            QuestManager.Instance.CheckDeliveryObjective(to, deliverableResources);*/
        } else {
            Debug.Log("No resources to deliver.");
        }

        // Update the UI after sending resources
        UiManager.Instance.hubUiManager.UpdateStorageUI();
    }

    public string GetEntityName() {
        return hubData.name;
    }

    public Transform GetTransform() {
        return transform;
    }

    #endregion

    #region UI Interaction Methods

    private void OnMouseDown() {
        UiManager.Instance.hubUiManager.ShowHubUi();
    }

    #endregion
}
