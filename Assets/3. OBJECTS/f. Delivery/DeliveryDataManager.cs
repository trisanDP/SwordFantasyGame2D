using System.Collections.Generic;
using UnityEngine;

public class DeliveryDataManager : MonoBehaviour {
    private List<DeliveryData> activeDeliveries = new List<DeliveryData>();

    public void AddDelivery(DeliveryData delivery) {
        activeDeliveries.Add(delivery);
    }

    public void RemoveDelivery(DeliveryData delivery) {
        activeDeliveries.Remove(delivery);
    }

    public List<DeliveryData> GetActiveDeliveries() {
        return new List<DeliveryData>(activeDeliveries);
    }

    public void UpdateDeliveryTime(DeliveryData delivery, float timeRemaining) {
        int index = activeDeliveries.IndexOf(delivery);
        if(index >= 0) {
            activeDeliveries[index].TimeRemaining = timeRemaining;
            // Optionally, notify the UI to refresh
            UiManager.Instance.deliveryUiManager.UpdateDeliveryEntry(delivery);
        }
    }
}
