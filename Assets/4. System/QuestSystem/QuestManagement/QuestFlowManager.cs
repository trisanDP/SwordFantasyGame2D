using UnityEngine;
using System.Collections.Generic;
using System.Resources;

public class QuestFlowManager : MonoBehaviour {
    public static QuestFlowManager Instance { get; private set; }
    private QuestDatabase questDatabase;
    private QuestManager questManager;
    private ResourceDatabase resourceDatabase;
   


    private void Awake() {
        if(Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if(!questDatabase)
            questDatabase = QuestManager.Instance.questDatabase;
        resourceDatabase = Resources.Load<ResourceDatabase>("ResourceDatabase");
        
    }

    // Called when the game starts
    private void Start() {
        CheckConditionForQuest1();
    }

    // Quest 1: Send 10 wood to NPC
    private bool IsQuestReadyToLaunch(Quest quest) {
        return quest != null && !quest.IsCompleted();
    }

    private void LaunchQuest(Quest quest) {
        if(IsQuestReadyToLaunch(quest)) {
            QuestManager.Instance.CheckQuestStatue(quest);
            Debug.Log($"Quest {quest.questID} launched.");
        } else {
            Debug.Log($"Quest {quest.questID} is already completed or null.");
        }
    }
    void CheckConditionForQuest1() {
        Quest quest = questDatabase.GetQuestByID("001");
/*        if(GameManager.Instance.timeManager.day == 1) {
            LaunchQuest(quest);
        }*/
    }
    private void CheckConditionsForQuest2() {
        Quest quest = questDatabase.GetQuestByID("002");
        int resourceAmount = quest.questGiver.storage.GetResourceAmount(resourceDatabase.GetResourceByName("wood"));
        if (quest.questGiver.storage.GetResourceAmount(resourceDatabase.GetResourceByName("wood")) >= 10){
            LaunchQuest(quest);
        }
    }
    // Quest 2: After collecting 15 wood
/*    public void CheckAndLaunchQuest2() {
        if(woodCollected >= 15) {
            Quest quest2 = QuestManager.Instance.GetQuestByID("quest2");
            if(quest2 != null) {
                QuestManager.Instance.ActivateQuest(quest2);
                Debug.Log("Quest 2 started: Collect 15 wood");
            }
        }
    }

    // Quest 3: After 3 in-game days pass
    public void CheckAndLaunchQuest3() {
        if(daysPassed >= 3) {
            Quest quest3 = QuestManager.Instance.GetQuestByID("quest3");
            if(quest3 != null) {
                QuestManager.Instance.ActivateQuest(quest3);
                Debug.Log("Quest 3 started: Collect resources after 3 days");
            }
        }
    }

    // Quest 4: All previous quests (Q1-Q3) completed within 4 days
    public void CheckAndLaunchQuest4() {
        if(daysPassed <= 4 && QuestManager.Instance.AreQuestsCompleted(new List<string> { "quest1", "quest2", "quest3" })) {
            Quest quest4 = QuestManager.Instance.GetQuestByID("quest4");
            if(quest4 != null) {
                QuestManager.Instance.ActivateQuest(quest4);
                Debug.Log("Quest 4 started: Complete Q1-Q3 in 4 days");
            }
        }
    }*/

}
