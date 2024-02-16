using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]
public class Quest
{

    QuestDatabase questDB;
    #region QuestDetail
    public int questID;
    public string questName;
    public string questDes;
    public string questTask;
    public string questRewardTxt;

    public QuestGoal goal;
    public Reward rewards;


    #endregion

    [Header("Status")]
    public bool HasAccepted;
    public bool HasRejected;
    public bool hasLaunched;
    public bool isActive;


    #region accepted
    public void Started()
    {
        questDB = Resources.Load<QuestDatabase>("QuestDatabase");
        HasAccepted = true;
        questDB.AcceptedQuests.Add(this);
        isActive = true;
    }

    public void Complete()
    {
        isActive = false;

    }
    #endregion


    public void Rejected()
    {
        HasRejected = true;

    }

}

#region RewardsSystem
[System.Serializable]
public class Reward
{
    public RewardType Type;
    public int Value;

    public Reward(RewardType type, int value)
    {
        Type = type;
        Value = value;

    }
    
    public void AddHealth()
    {
        GameManager.Instance.playerScript.playerHealth.AddHealth(this.Value);
    }

    public void AddCoin()
    {
        Debug.Log("Add Coin");
    }

    public void AddItem()
    {
        
        Debug.Log("Item Added");
    }
}

public enum RewardType
{
    IncreaseHealth,
    AddCoin,
    AddItem
}
#endregion
