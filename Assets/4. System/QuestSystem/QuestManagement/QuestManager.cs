using UnityEngine;
using System.Collections.Generic;

public class QuestManager : MonoBehaviour {
    public static QuestManager Instance { get; private set; }

    // Store active, completed, and available quests
    public List<Quest> activeQuests = new List<Quest>();
    public List<Quest> completedQuests = new List<Quest>();
    public List<Quest> availableQuests = new List<Quest>();

    // Reference to quest database (for database-stored quests)
    public QuestDatabase questDatabase;

    private void Awake() {
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        // Singleton pattern to ensure only one instance
        if(Instance != null && Instance != this) {
            Destroy(this.gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start() {
        // Initialize available quests from the quest database and scriptable quests
        LoadAvailableQuests();
    }

    // Load all available quests from the database
    private void LoadAvailableQuests() {
        if(questDatabase != null) {
            availableQuests.AddRange(questDatabase.quests); // Add database quests
        }

        // Optionally, you could add ScriptableObject quests if needed
        // Find ScriptableObject quests in the Resources folder (optional)
        Quest[] scriptableQuests = Resources.LoadAll<Quest>("Quests");
        availableQuests.AddRange(scriptableQuests);
    }

    // Start a new quest
    public void StartQuest(Quest quest) {
        if(!activeQuests.Contains(quest) && !completedQuests.Contains(quest)) {
            activeQuests.Add(quest);
            Debug.Log($"Quest Started: {quest.questName}");
        } else {
            Debug.LogWarning($"Quest {quest.questName} is already active or completed.");
        }
    }

    // Complete a quest
    public void CompleteQuest(Quest quest) {
        if(activeQuests.Contains(quest)) {
            activeQuests.Remove(quest);
            completedQuests.Add(quest);
            Debug.Log($"Quest Completed: {quest.questName}");

            // Check and unlock quests that depend on this quest
            CheckForUnlockableQuests(quest);
        } else {
            Debug.LogWarning($"Quest {quest.questName} is not active.");
        }
    }

    // Check for dependent quests that can now be unlocked
    private void CheckForUnlockableQuests(Quest completedQuest) {
        foreach(Quest quest in availableQuests) {
            if(quest.dependentQuests != null && quest.dependentQuests.Count > 0) {
                bool canUnlock = true;

                foreach(Quest dependency in quest.dependentQuests) {
                    if(!completedQuests.Contains(dependency)) {
                        canUnlock = false;
                        break;
                    }
                }

                if(canUnlock && !activeQuests.Contains(quest) && !completedQuests.Contains(quest)) {
                    activeQuests.Add(quest);
                    Debug.Log($"New quest unlocked: {quest.questName}");
                }
            }
        }
    }

    // Check if a quest is completed
    public bool IsQuestCompleted(string questID) {
        return completedQuests.Exists(q => q.questID == questID);
    }

    // Check if a quest is active
    public bool IsQuestActive(string questID) {
        return activeQuests.Exists(q => q.questID == questID);
    }

    // Get active quest by ID
    public Quest GetActiveQuest(string questID) {
        return activeQuests.Find(q => q.questID == questID);
    }

    // Get completed quest by ID
    public Quest GetCompletedQuest(string questID) {
        return completedQuests.Find(q => q.questID == questID);
    }

    // Reset all quests (optional, useful for debugging or resetting progress)
    public void ResetQuests() {
        activeQuests.Clear();
        completedQuests.Clear();
        LoadAvailableQuests(); // Reload all available quests
    }

    // Debugging tool to print active and completed quests
    public void PrintQuestStatus() {
        Debug.Log("Active Quests:");
        foreach(Quest quest in activeQuests) {
            Debug.Log(quest.questName);
        }

        Debug.Log("Completed Quests:");
        foreach(Quest quest in completedQuests) {
            Debug.Log(quest.questName);
        }
    }
}
