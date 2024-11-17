using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DeliveryUIManager : MonoBehaviour {
    [Header("UI Elements")]
    public GameObject deliveryUiPanel; // Panel containing the delivery UI
    public GameObject deliveryEntryPrefab; // Prefab for individual delivery entries
    public Transform deliveryContent; // Parent object for delivery entries (inside a Scroll View)

    private List<DeliveryData> activeDeliveries = new List<DeliveryData>(); // List of active deliveries

    private void Awake() {
        HideDeliveryUIList();
        foreach(Transform child in deliveryContent) {
            Destroy(child.gameObject);
        }
    }

    // Show the delivery UI panel
    public void ShowDeliveryUIList() {
        deliveryUiPanel.SetActive(true);
        RefreshUI();
    }

    // Hide the delivery UI panel
    public void HideDeliveryUIList() {
        deliveryUiPanel.SetActive(false);
    }

    // Add a new delivery entry to the UI
    public void AddDeliveryEntry(DeliveryData deliveryData) { 
        DeliveryEntry newEntry = Instantiate(deliveryEntryPrefab, deliveryContent).GetComponent<DeliveryEntry>();
        if(newEntry != null) {
            newEntry.Initialize(deliveryData.SenderName, deliveryData.ReceiverName, deliveryData.TotalTime);
            activeDeliveries.Add(deliveryData); // Track active deliveries
        } else {
            Debug.LogError("Failed to create a new delivery entry.");
        }
    }

    // Update an existing delivery entry in the UI
    public void UpdateDeliveryEntry(DeliveryData deliveryData) {

        foreach(var entry in deliveryContent.GetComponentsInChildren<DeliveryEntry>()) {
            if(entry.senderText.text == deliveryData.SenderName && entry.receiverText.text == deliveryData.ReceiverName) {
                entry.UpdateTime(deliveryData.TimeRemaining);
                break;
            }
        }
    }

    // Remove a delivery entry from the UI
    public void RemoveDeliveryEntry(DeliveryData deliveryData) {
        foreach(var entry in deliveryContent.GetComponentsInChildren<DeliveryEntry>()) {
            if(entry.senderText.text == deliveryData.SenderName && entry.receiverText.text == deliveryData.ReceiverName) {
                Destroy(entry.gameObject);
                activeDeliveries.Remove(deliveryData); // Remove from active deliveries
                break;
            }
        }
    }

    // Refresh the UI to reflect current active deliveries
    private void RefreshUI() {
        foreach(Transform child in deliveryContent) {
            Destroy(child.gameObject);
        }

        foreach(var delivery in activeDeliveries) {
            AddDeliveryEntry(delivery);
        }
    }
}
