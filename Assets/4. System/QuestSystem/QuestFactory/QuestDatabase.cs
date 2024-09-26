using UnityEngine;
using System.Collections.Generic;
using UnityEditor;


    [CreateAssetMenu(fileName = "QuestDatabase", menuName = "Quest System/Quest Database")]
    public class QuestDatabase : ScriptableObject {

        public List<Quest> quests;

        public Quest GetQuestByID(string questID) {
            return quests.Find(quest => quest.questID == questID);
        }

        // Checks if a quest in a specific NPC's chain is complete
        public bool IsQuestComplete(string questID) {
            var quest = GetQuestByID(questID);
            return quest != null && quest.IsCompleted();
        }


    }
