using System.Collections.Generic;
using UnityEngine;
/*
public class ResourceDelivery : MonoBehaviour {
    public GameObject deliverySpritePrefab; // Prefab for the delivery sprite
    public float timePerUnit = 10f; // Time taken to deliver each unit of resource
    public DeliveryUIManager deliveryUIManager;

    private void Start() {
        deliveryUIManager = UiManager.Instance.hubUiManager.deliveryUIManager;
    }

    // Deliver resources from one entity to another
    public void DeliverResources(IResourceEntity sender, IResourceEntity receiver, Dictionary<ResourceType, int> resourcePackage, Vector3 senderPosition, Vector3 receiverPosition) {
        // Start the delivery process using a coroutine
        StartCoroutine(DeliverCoroutine(sender, receiver, resourcePackage, senderPosition, receiverPosition));
    }

    private IEnumerator<WaitForEndOfFrame> DeliverCoroutine(IResourceEntity sender, IResourceEntity receiver, Dictionary<ResourceType, int> resourcePackage, Vector3 senderPosition, Vector3 receiverPosition) {
        // Instantiate a delivery sprite to represent the delivery visually
        GameObject deliverySprite = Instantiate(deliverySpritePrefab, senderPosition, Quaternion.identity);

        // Calculate the total delivery time based on the number of resources
        float totalDeliveryTime = CalculateTotalDeliveryTime(resourcePackage);
        float journeyLength = Vector3.Distance(senderPosition, receiverPosition);
        float startTime = Time.time;

        // Add a new delivery entry to the UI (this will queue it if UI is inactive)
        int deliveryIndex = UiManager.Instance.hubUiManager.deliveryUIManager.AddDeliveryEntry(sender.GetEntityName(), receiver.GetEntityName(), totalDeliveryTime);

        // Animate the delivery sprite moving from sender to receiver
        while(Vector3.Distance(deliverySprite.transform.position, receiverPosition) > 0.1f) {
            float distCovered = (Time.time - startTime) * (journeyLength / totalDeliveryTime); // Speed of delivery
            float fractionOfJourney = distCovered / journeyLength;

            deliverySprite.transform.position = Vector3.Lerp(senderPosition, receiverPosition, fractionOfJourney);

            // Update the time remaining in the UI if it's active
            float timeRemaining = totalDeliveryTime - (Time.time - startTime);
            if(UiManager.Instance.hubUiManager.deliveryUIManager.IsContentActive()) {
                UiManager.Instance.hubUiManager.deliveryUIManager.UpdateDeliveryEntry(deliveryIndex, timeRemaining);
            }

            yield return null;
        }

        // Destroy the delivery sprite after the delivery is complete
        Destroy(deliverySprite);

        // Deliver the resources to the receiver
        foreach(var entry in resourcePackage) {
            ResourceType resource = entry.Key;
            int amount = entry.Value;

            // Call the receiver's method to receive resources
            receiver.ReceiveResources(resource, amount);
        }

        // Optionally notify the completion of delivery
        Debug.Log("Resource delivery completed.");

        // Remove the entry from the UI if it's active
        if(UiManager.Instance.hubUiManager.deliveryUIManager.IsContentActive()) {
            UiManager.Instance.hubUiManager.deliveryUIManager.RemoveDeliveryEntry(deliveryIndex);
        }
    }

    // Calculate the total delivery time based on the resources in the package
    private float CalculateTotalDeliveryTime(Dictionary<ResourceType, int> resourcePackage) {
        int totalUnits = 0;

        // Sum up all units of resources in the package
        foreach(var entry in resourcePackage) {
            totalUnits += entry.Value;
        }

        // Calculate total time based on units and time per unit
        return totalUnits * timePerUnit;
    }

    // On clicking the GameObject, activate the delivery UI window
    private void OnMouseDown() {*//*
        UiManager.Instance.hubUiManager.deliveryUIManager.SetActive(true);*//*
    }
}*/
