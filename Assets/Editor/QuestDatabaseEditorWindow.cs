using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

    public class QuestDatabaseEditorWindow : EditorWindow {
        private QuestDatabase questDatabase;
        private Quest selectedQuest;
        private Vector2 scrollPos;

        [MenuItem("Quest System/Quest Database Editor")]
        public static void ShowWindow() {
            GetWindow<QuestDatabaseEditorWindow>("Quest Database Editor");
        }

        private void OnEnable() {
            questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
            if(questDatabase == null) {
                Debug.LogError("QuestDatabase not found in Resources.");
            }
        }

        private void OnGUI() {
            GUILayout.Label("Quest Database Editor", EditorStyles.boldLabel);

            if(questDatabase == null) {
                GUILayout.Label("No Quest Database found.");
                return;
            }

            // List all quests in the database
            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            foreach(var quest in questDatabase.quests) {
                if(GUILayout.Button(quest.questName)) {
                    selectedQuest = quest;
                }
            }
            EditorGUILayout.EndScrollView();

            // Show selected quest details
            if(selectedQuest != null) {
                EditorGUILayout.LabelField("Quest Details", EditorStyles.boldLabel);
                selectedQuest.questName = EditorGUILayout.TextField("Quest Name", selectedQuest.questName);
                selectedQuest.description = EditorGUILayout.TextField("Description", selectedQuest.description);

                // Display dependent quests
                EditorGUILayout.LabelField("dependentQuest");
                for(int i = 0; i < selectedQuest.dependentQuests.Count; i++) {
                    selectedQuest.dependentQuests[i] = (Quest)EditorGUILayout.ObjectField("Dependency " + (i + 1), selectedQuest.dependentQuests[i], typeof(Quest), false);
                }

                if(GUILayout.Button("Add Dependency")) {
                    selectedQuest.dependentQuests.Add(null);
                }

                if(GUILayout.Button("Remove Selected Quest")) {
                    RemoveQuest(selectedQuest);
                }

                if(GUILayout.Button("Save Quest")) {
                    EditorUtility.SetDirty(selectedQuest);
                    AssetDatabase.SaveAssets();
                }
            }

            if(GUILayout.Button("Create New Quest")) {
                CreateNewQuest();
            }
        }

        private void CreateNewQuest() {
            Quest newQuest = CreateInstance<Quest>();
            newQuest.questID = System.Guid.NewGuid().ToString();
            newQuest.questName = "New Quest";
            newQuest.description = "Description here.";
            newQuest.rewards = new List<Reward>();
            newQuest.dependentQuests = new List<Quest>();

            AssetDatabase.CreateAsset(newQuest, $"Assets/Resources/Quests/NewQuest_{newQuest.questID}.asset");
            AssetDatabase.SaveAssets();
            questDatabase.quests.Add(newQuest);
            EditorUtility.SetDirty(questDatabase);
        }

        private void RemoveQuest(Quest questToRemove) {
            if(questDatabase.quests.Contains(questToRemove)) {
                questDatabase.quests.Remove(questToRemove);
                AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(questToRemove));
                AssetDatabase.SaveAssets();
                selectedQuest = null; // Deselect the removed quest
                Debug.Log($"Removed Quest: {questToRemove.questName}");
            } else {
                Debug.LogWarning("Quest not found in database.");
            }
        }
    }
