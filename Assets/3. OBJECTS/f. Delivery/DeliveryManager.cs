using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeliveryManager : MonoBehaviour {
    public static DeliveryManager instance;

    [Header("Delivery Settings")]
    public GameObject deliverySpritePrefab; // Prefab for the delivery sprite
    public float timePerUnit = 10f; // Time taken to deliver each unit of resource
    public DeliveryUIManager deliveryUIManager; // Reference to the UI Manager
    public GameManager gameManager;

    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        if(deliveryUIManager == null) {
            Debug.LogError("DeliveryUIManager is not assigned in DeliveryManager.");
        }
    }

    // Method to start the delivery process
    public void DeliverResources(IResourceEntity sender, IResourceEntity receiver, Dictionary<ResourceType, int> resourcePackage, Vector3 senderPosition, Vector3 receiverPosition) {
        if(receiver == null) {
            Debug.LogError("Receiver is null. Delivery cannot proceed.");
            return;
        }

        // Start the delivery coroutine
        StartCoroutine(DeliverCoroutine(sender, receiver, resourcePackage, senderPosition, receiverPosition));
    }

    // Coroutine to handle the delivery process
    private IEnumerator DeliverCoroutine(IResourceEntity sender, IResourceEntity receiver, Dictionary<ResourceType, int> resourcePackage, Vector3 senderPosition, Vector3 receiverPosition) {
        GameObject deliverySprite = Instantiate(deliverySpritePrefab, senderPosition, Quaternion.identity);
        float totalDeliveryTime = CalculateTotalDeliveryTime(resourcePackage);
        float startTime = Time.time;

        // Create a DeliveryData object
        DeliveryData deliveryData = new DeliveryData(sender.GetEntityName(), receiver.GetEntityName(), totalDeliveryTime);

        // Add a new delivery entry to the UI
        deliveryUIManager.AddDeliveryEntry(deliveryData);

        while(Vector3.Distance(deliverySprite.transform.position, receiverPosition) > 0.1f) {
            if(!GameManager.Instance.isPaused) {
                float elapsedTime = Time.time - startTime;
                float fractionOfJourney = Mathf.Clamp01(elapsedTime / totalDeliveryTime);

                deliverySprite.transform.position = Vector3.Lerp(senderPosition, receiverPosition, fractionOfJourney);

                // Update time remaining in the UI
                float timeRemaining = totalDeliveryTime - elapsedTime;
                deliveryData.TimeRemaining = timeRemaining; // Update the delivery data
                deliveryUIManager.UpdateDeliveryEntry(deliveryData); // Update the UI
            }

            yield return null;
        }

        deliverySprite.transform.position = receiverPosition;
        Destroy(deliverySprite);

        // Deliver resources to the receiver
        receiver.ReceiveResources(resourcePackage, sender);
        if(QuestManager.Instance.IsQuestActive())
            QuestManager.Instance.CheckReceivedDelivery(receiver, resourcePackage);
        // Quest tracking: Update the quest objectives after resources are delivered


        // Remove the entry from the UI after delivery is completed
        deliveryUIManager.RemoveDeliveryEntry(deliveryData);
    }

    // Calculate the total delivery time based on the resource package
    private float CalculateTotalDeliveryTime(Dictionary<ResourceType, int> resourcePackage) {
        int totalUnits = 0;

        foreach(var entry in resourcePackage) {
            totalUnits += entry.Value;
        }

        return totalUnits * timePerUnit;
    }
}
