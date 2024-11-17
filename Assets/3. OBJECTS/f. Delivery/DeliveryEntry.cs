using UnityEngine;
using TMPro;

public class DeliveryEntry : MonoBehaviour {
    [Header("UI Elements")]
    public TextMeshProUGUI senderText; // UI Text for the sender's name
    public TextMeshProUGUI receiverText; // UI Text for the receiver's name
    public TextMeshProUGUI timeRemainingText; // UI Text for the time remaining

    private float totalTime; // Total delivery time

    // Initialize the entry with sender, receiver names, and total delivery time
    public void Initialize(string senderName, string receiverName, float totalTime) {
        senderText.text = senderName;
        receiverText.text = receiverName;
        this.totalTime = totalTime;
        UpdateTime(totalTime); // Set initial time remaining
    }

    // Update the time remaining display
    public void UpdateTime(float timeRemaining) {
        int minutes = Mathf.FloorToInt(timeRemaining / 60);
        int seconds = Mathf.FloorToInt(timeRemaining % 60);
        timeRemainingText.text = $"{minutes:D2}:{seconds:D2}";
    }
}



[System.Serializable]
public class DeliveryData {
    public string SenderName { get; private set; } // Name of the sender
    public string ReceiverName { get; private set; } // Name of the receiver
    public float TotalTime { get; private set; } // Total time for the delivery
    public float TimeRemaining { get; set; } // Time remaining for the delivery

    // Constructor to initialize the delivery data
    public DeliveryData(string senderName, string receiverName, float totalTime) {
        SenderName = senderName;
        ReceiverName = receiverName;
        TotalTime = totalTime;
        TimeRemaining = totalTime; // Initialize time remaining to total time
    }
}



