using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class QuestManager : MonoBehaviour {
    public static QuestManager Instance { get; private set; }

    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();

    public QuestDatabase questDatabase;

    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if(questDatabase == null)
            questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        
    }

    public void UpdateQuestTimes(float hoursPassed) {
        foreach(Quest quest in activeQuests) {
            if(quest.status == Quest.QuestStatus.Active) {
                quest.timeRemaining -= hoursPassed;
                if(quest.timeRemaining <= 0) {
                    HandleQuestTimeUp(quest);
                }
            }
        }
    }

    // Handle quest timeout
    private void HandleQuestTimeUp(Quest quest) {
        quest.status = Quest.QuestStatus.Failed; 
        Debug.Log($"Quest {quest.questName} has failed due to time running out.");
    }


    #region Quest_Main_Activate_and_Complete

    public bool IsQuestActive() {
        if(activeQuests.Count > 0)
            return true;
        return false;
    }

    public void CheckQuestStatue(Quest quest) {
        if(!completedQuests.Contains(quest) && quest.IsCompleted()) {
            CompleteQuest(quest);
            return;
        } else if(activeQuests.Contains(quest)) {
            /*            Debug.Log("Hello");*/
            return;
        } else {
            ActivateQuest(quest);
            return;
        }
    }
    private void ActivateQuest(Quest quest) {
        activeQuests.Add(quest);
/*        Debug.Log("This One:" + quest.objectives[0].npcData);*/
    }

    // Completes a quest if all its objectives are finished
    private void CompleteQuest(Quest quest) {
        activeQuests.Remove(quest);
        completedQuests.Add(quest);
        ForwardReward(quest);
    }

    #endregion

    //------------------------------------------------------------------------
    #region HandleRewards
    void ForwardReward(Quest quest) {
        Dictionary<ResourceType, int> resourceRewards = new Dictionary<ResourceType, int>();
        int totalCredits = 0;
        List<string> itemRewards = new List<string>();

        // Loop through all the rewards in the quest and categorize them
        foreach(Reward reward in quest.rewards) {
            switch(reward.rewardType) {
                case RewardType.Resource:
                AddResourceReward(reward, resourceRewards);
                break;
                case RewardType.Credit:
                totalCredits += reward.amount;
                break;
                case RewardType.Item:
                itemRewards.Add($"{reward.itemID} x{reward.amount}");
                break;
                default:
                Debug.LogWarning("Unknown reward type found in quest: " + quest.questName);
                break;
            }
        }

        // Distribute the rewards only if they are not empty

        // Distribute resources only if there are any
        if(resourceRewards.Count > 0) {
            DistributeResources(resourceRewards, quest.questGiver);
        }

        // Add credits only if there are any
        if(totalCredits > 0) {
            AddCredits(totalCredits);
        }

        // Distribute items only if the list is not empty
        if(itemRewards.Count > 0) {
            DistributeItems(itemRewards);
        }

        Debug.Log("All rewards distributed for quest: " + quest.questName);
    }

    void AddResourceReward(Reward reward, Dictionary<ResourceType, int> resourceRewards) {
        if(resourceRewards.ContainsKey(reward.resourceType)) {
            resourceRewards[reward.resourceType] += reward.amount;
        } else {
            resourceRewards.Add(reward.resourceType, reward.amount);
        }
    }


    void DistributeResources(Dictionary<ResourceType, int> resourceRewards, NPCData questGiver) {
        questGiver.conectedNPC.SendResources(resourceRewards,GameManager.Instance.hub);
        Debug.Log($"Added resources to player's hub.");

    }

    void AddCredits(int amount) {
        // Assuming you have a player currency manager
        GameManager.Instance.hub.hubData.hubstorage.credits += amount;
        Debug.Log($"Added {amount} credits to player's account.");

    }

    void DistributeItems(List<string> itemRewards) {
        foreach(string item in itemRewards) {
            // Parse item string to extract ID and amount, if needed
            string[] itemData = item.Split('x');
            string itemID = itemData[0].Trim();
            int amount = int.Parse(itemData[1].Trim());

            // Assuming you have an inventory system to manage player's items
            /*            PlayerInventory.Instance.AddItem(itemID, amount);*/ // Add Item Logic Here

            Debug.Log($"Added {amount} of {itemID} to player's inventory.");
        }
    }

    #endregion

    //----------------------------------------------------------------------------------------

    #region QuestProgressCheckMethod
    public void CheckReceivedDelivery(IResourceEntity receiver, Dictionary<ResourceType, int> deliveredResources) {
        foreach(Quest quest in activeQuests) {
            foreach(QuestObjective objective in quest.objectives) {
                if(objective.type == QuestObjective.ObjectiveType.Deliver && objective.npcData.npcName == receiver.GetEntityName()) {
                    foreach(var deliveredResource in deliveredResources) {
                        if(objective.resourceType == deliveredResource.Key) {
                            objective.UpdateReceivedAmount(deliveredResource.Value);
                            CheckQuestStatue(quest);

                            if(objective.isCompleted) {
                                Debug.Log($"Objective '{objective.description}' for quest '{quest.questID}' is now completed.");
                            }
                            break; // Stop checking once this resource is handled
                        }
                    }
                }
            }
        }
    }

    #endregion

}