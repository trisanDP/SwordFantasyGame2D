using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {
    #region Properties
    public string questID;
    public string questName;
    public string description;
    public NPCData questGiver;
    public bool isRepeatable;

    public int timeLimit; // Time limit for quest (if needed)
    public float timeRemaining; // Time remaining in hours

    public List<QuestObjective> objectives = new List<QuestObjective>();
    public List<Reward> rewards = new List<Reward>();

    public QuestStatus status;
    #endregion

    #region Enums
    public enum QuestStatus { Active, Completed, Failed }
    #endregion

    #region Quest Lifecycle

    // Called when a quest is activated
    public void StartQuest() {
        status = QuestStatus.Active;
        ResetQuest();  // Reset all objectives
    }

    // Check if the quest is completed by verifying all objectives
    public bool IsCompleted() {
        foreach(var objective in objectives) {
            if(!objective.IsCompleted())
                return false;
        }

        status = QuestStatus.Completed;
        Debug.Log(questID + " Completed");
        return true;
    }

    // Reset quest state for reuse (e.g., in testing or if repeatable)
    public void ResetQuest() {
        foreach(var objective in objectives) {
            objective.ResetObjective();
        }
        status = QuestStatus.Active; // Reset status to Active
    }

    #endregion

    #region Rewards Management

    // Get a formatted string of rewards for UI or logs
    public string GetRewardsTxt() {
        if(rewards == null || rewards.Count == 0)
            return "No rewards";

        string rewardText = "";
        foreach(var reward in rewards) {
            rewardText += reward.GetRewardText() + "\n";
        }

        return rewardText.TrimEnd(); // Remove trailing newline
    }

    #endregion

    #region Debugging & Utility Functions
    // This region could include additional functions related to quest debugging
    // For example, log the quest's state, rewards, or objectives
    #endregion
}


[System.Serializable]
public class QuestObjective {
    public string description;
    public ObjectiveType type;

    public int targetAmount; // For Collect, Kill, Build objectives
    public int currentAmount; // Tracks progress
    public ResourceType resourceType; // For Deliver objectives
    public NPCData npcData; // Target NPC or Hub
    public bool isCompleted;

    // Reset objective to initial state
    public void ResetObjective() {
        currentAmount = 0;
        isCompleted = false;
    }



    // Check if the objective is completed
    public bool IsCompleted() {
        switch(type) {
            case ObjectiveType.Deliver:
            return currentAmount >= targetAmount; // Ensure NPC has received enough resources

            default:
            return false;
        }
    }

    // Update the received amount for delivery objectives
    public void UpdateReceivedAmount(int amountReceived) {
        currentAmount += amountReceived;
        if(currentAmount >= targetAmount) {
            isCompleted = true;
/*            Debug.Log($"Updated objective for quest, new amount: {currentAmount}");*/
        }
    }

    public enum ObjectiveType {
        None,
        Collect,
        Deliver
    }
}





[System.Serializable]
public class Reward {
    public RewardType rewardType;
    public ResourceType resourceType; // Used if rewardType is Resource
    public string itemID; // Used if rewardType is Item
    public int amount; // For Resource, Item, and Credit
    public int selectedResourceIndex; // Index for selecting specific ResourceType

    // Get the resource name based on the selected index
    public string GetResourceName() {
        ResourceType[] resources = Resources.LoadAll<ResourceType>("ResourceTypes");
        if(resources != null && selectedResourceIndex >= 0 && selectedResourceIndex < resources.Length) {
            return resources[selectedResourceIndex].resourceName;
        }
        return "Unknown Resource";
    }

    // Get reward as a formatted string
    public string GetRewardText() {
        switch(rewardType) {
            case RewardType.Resource:
            return $"Resource: {GetResourceName()} x{amount}";
            case RewardType.Item:
            return $"Item: {itemID} x{amount}";
            case RewardType.Credit:
            return $"Credit: {amount}";
            default:
            return "Unknown Reward";
        }
    }
}

public enum RewardType {
    Resource, // e.g., Wood, Stone, etc.
    Item,     // e.g., Weapon, Armor, etc.
    Credit    // In-game currency
}
