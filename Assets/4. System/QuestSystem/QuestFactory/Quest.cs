using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest System/Quest")]

[System.Serializable]
public class Quest : ScriptableObject {
    public string questID;
    public string questName;
    public string description;
    public List<Quest> dependentQuests;


    // Quest criteria (you can extend this)
    public List<QuestObjective> objectives;
    public List<Reward> rewards;

    public bool isRepeatable;

    // Rewards for completing the quest

    public bool IsCompleted() {
        foreach(var objective in objectives) {
            if(!objective.isCompleted) return false;
        }
        return true;
    }

    public string GetRewardsTxt() {
        string a = "";

        foreach(var reward in rewards) {
            a += reward.itemID.ToString() + "\n";
        }

        return a;
    }
}


[System.Serializable]
public class QuestObjective {
    public string description;
    public bool isCompleted;
    public ObjectiveType type;
    public int targetAmount;
    public int currentAmount;
}

public enum ObjectiveType {
    Collect,
    Kill,
    Deliver,
    Build
}

[System.Serializable]
public class Reward {
    public string itemID;
    public int quantity;


}
