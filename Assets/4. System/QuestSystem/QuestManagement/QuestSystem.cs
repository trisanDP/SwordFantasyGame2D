using UnityEngine;
using System.Collections.Generic;

    public class QuestSystem : MonoBehaviour {
        public QuestDatabase questDatabase;  // Reference to the quest database
        private List<Quest> activeQuests = new List<Quest>();
        private List<Quest> completedQuests = new List<Quest>();

        private void Start() {
            if(questDatabase == null) {
                questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
            }

            if(questDatabase == null) {
                Debug.LogError("Quest Database not found.");
            }
        }

        /// <summary>
        /// Assigns a quest if its dependentQuest are completed.
        /// </summary>
        public void AssignQuest(Quest quest) {
            if(CanStartQuest(quest)) {
                StartQuest(quest);
                Debug.Log($"Started Quest: {quest.questName}");
            } else {
                Debug.LogWarning($"Cannot start quest {quest.questName}, dependent quests not completed.");
            }
        }

        /// <summary>
        /// Checks if a quest can be started (i.e., its dependent quests are completed).
        /// </summary>
        private bool CanStartQuest(Quest quest) {
            foreach(var depQuest in quest.dependentQuests) {
                if(!IsQuestCompleted(depQuest)) {
                    return false; // If any dependent quest is not completed, this quest can't start
                }
            }
            return true;
        }

        /// <summary>
        /// Marks a quest as completed and assigns rewards.
        /// </summary>
        public void CompleteQuest(Quest quest) {
            if(IsQuestActive(quest) && quest.IsCompleted()) {
                activeQuests.Remove(quest);
                completedQuests.Add(quest);
                GiveRewards(quest.rewards);
                Debug.Log($"Quest {quest.questName} completed.");
            }
        }

        private void GiveRewards(List<Reward> rewards) {
            foreach(var reward in rewards) {
                Debug.Log($"Reward: {reward.itemID}, Quantity: {reward.quantity}");
                // Reward logic here
            }
        }

        private bool IsQuestActive(Quest quest) => activeQuests.Contains(quest);
        private bool IsQuestCompleted(Quest quest) => completedQuests.Contains(quest);

        private void StartQuest(Quest quest) {
            if(!IsQuestActive(quest)) {
                activeQuests.Add(quest);
            }
        }
    }
