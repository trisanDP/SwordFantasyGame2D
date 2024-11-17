using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class QuestReviewWindow : EditorWindow {
    private QuestDatabase questDatabase;
    private int selectedQuestIndex = -1;
    private Vector2 leftPanelScrollPos;
    private Vector2 rightPanelScrollPos;
    private Quest selectedQuest;
    private string[] questListNames = { "All Quests", "Day Quests", "Night Quests" };
    private int selectedQuestListIndex = 0;

    private bool showObjectives = true;
    private bool showRewards = true;

    [MenuItem("Quest System/Quest Review")]
    public static void ShowWindow() {
        GetWindow<QuestReviewWindow>("Quest Review");
    }

    private void OnEnable() {
        LoadQuestDatabase();
    }

    private void LoadQuestDatabase() {
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");
        if(questDatabase == null) {
            Debug.LogError("QuestDatabase not found in Resources.");
        }
    }

    private void OnGUI() {
        if(questDatabase == null) {
            EditorGUILayout.LabelField("No Quest Database found.");
            return;
        }

        EditorGUILayout.BeginHorizontal();

        // Left side: Quest list panel
        DrawLeftPanel();

        // Visual separator
        EditorGUILayout.Space(10); // Add some spacing for visual separation

        // Right side: Quest detail panel
        DrawRightPanel();

        EditorGUILayout.EndHorizontal();
    }

    private void DrawLeftPanel() {
        EditorGUILayout.BeginVertical("box", GUILayout.Width(250));
        leftPanelScrollPos = EditorGUILayout.BeginScrollView(leftPanelScrollPos);

        GUILayout.Label("Quest List", EditorStyles.boldLabel);

        // Dropdown to select between All, Day, Night Quests
        selectedQuestListIndex = EditorGUILayout.Popup("Quest List", selectedQuestListIndex, questListNames);

        // Depending on selected dropdown option, display different quest lists
        List<Quest> currentQuestList = GetCurrentQuestList();
        if(currentQuestList != null) {
            for(int i = 0; i < currentQuestList.Count; i++) {
                if(GUILayout.Button($"{currentQuestList[i].questID}_{currentQuestList[i].questName}")) {
                    selectedQuestIndex = i;
                    selectedQuest = currentQuestList[i];
                }
            }
        } else {
            EditorGUILayout.LabelField("No quests available.");
        }

        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();
    }

    private void DrawRightPanel() {
        if(selectedQuest == null) {
            EditorGUILayout.LabelField("Select a quest from the list to view details.");
            return;
        }

        rightPanelScrollPos = EditorGUILayout.BeginScrollView(rightPanelScrollPos);

        GUILayout.Label("Quest Details", EditorStyles.boldLabel);

        selectedQuest.questName = EditorGUILayout.TextField("Quest Name", selectedQuest.questName);
        selectedQuest.description = EditorGUILayout.TextField("Description", selectedQuest.description);

        GUILayout.Label("Quest Giver", EditorStyles.boldLabel);
        selectedQuest.questGiver = (NPCData)EditorGUILayout.ObjectField("Quest Giver NPC", selectedQuest.questGiver, typeof(NPCData), false);

        // Collapsible Objectives section
        showObjectives = EditorGUILayout.Foldout(showObjectives, "Objectives");
        if(showObjectives) {
            DrawObjectivesSection();
        }

        // Collapsible Rewards section
        showRewards = EditorGUILayout.Foldout(showRewards, "Rewards");
        if(showRewards) {
            DrawRewardsSection();
        }

        // Button to save changes
        if(GUILayout.Button("Save Quest Changes")) {
            SaveQuestChanges();
        }        
        if(GUILayout.Button("Delete Quest")) {
            DeleteSelectedQuest();
        }

        EditorGUILayout.EndScrollView();
    }

    private void DrawObjectivesSection() {
        List<int> objectivesToRemove = new List<int>();

        EditorGUILayout.BeginVertical("box");
        for(int i = 0; i < selectedQuest.objectives.Count; i++) {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Objective {i + 1}", EditorStyles.boldLabel);
            if(GUILayout.Button("Remove", GUILayout.Width(70))) {
                objectivesToRemove.Add(i);
            }
            EditorGUILayout.EndHorizontal();

            QuestObjective objective = selectedQuest.objectives[i];
            objective.description = EditorGUILayout.TextField("Description", objective.description);
            objective.type = (QuestObjective.ObjectiveType)EditorGUILayout.EnumPopup("Objective Type", objective.type);

            if(objective.type == QuestObjective.ObjectiveType.Collect || objective.type == QuestObjective.ObjectiveType.Deliver) {
                objective.resourceType = (ResourceType)EditorGUILayout.ObjectField("Resource Type", objective.resourceType, typeof(ResourceType), false);
                objective.targetAmount = EditorGUILayout.IntField("Target Amount", objective.targetAmount);
            }

            if(objective.type == QuestObjective.ObjectiveType.Deliver) {
                objective.npcData = (NPCData)EditorGUILayout.ObjectField("Target NPC", objective.npcData, typeof(NPCData), false);
            }

            EditorGUILayout.EndVertical();
        }

        foreach(int index in objectivesToRemove) {
            selectedQuest.objectives.RemoveAt(index);
        }

        if(GUILayout.Button("Add Objective", GUILayout.Width(150))) {
            selectedQuest.objectives.Add(new QuestObjective());
        }

        EditorGUILayout.EndVertical();
    }

    private void DrawRewardsSection() {
        List<int> rewardsToRemove = new List<int>();

        EditorGUILayout.BeginVertical("box");
        for(int i = 0; i < selectedQuest.rewards.Count; i++) {
            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Reward {i + 1}", EditorStyles.boldLabel);
            if(GUILayout.Button("Remove", GUILayout.Width(70))) {
                rewardsToRemove.Add(i);
            }
            EditorGUILayout.EndHorizontal();

            Reward reward = selectedQuest.rewards[i];
            reward.rewardType = (RewardType)EditorGUILayout.EnumPopup("Reward Type", reward.rewardType);

            if(reward.rewardType == RewardType.Resource) {
                reward.resourceType = (ResourceType)EditorGUILayout.ObjectField("Resource Type", reward.resourceType, typeof(ResourceType), false);
                reward.amount = EditorGUILayout.IntField("Amount", reward.amount);
            }

            EditorGUILayout.EndVertical();
        }

        foreach(int index in rewardsToRemove) {
            selectedQuest.rewards.RemoveAt(index);
        }

        if(GUILayout.Button("Add Reward", GUILayout.Width(150))) {
            selectedQuest.rewards.Add(new Reward());
        }

        EditorGUILayout.EndVertical();
    }

    private void SaveQuestChanges() {
        List<Quest> currentQuestList = GetCurrentQuestList();

        if(selectedQuestIndex >= 0 && selectedQuestIndex < currentQuestList.Count) {
            currentQuestList[selectedQuestIndex] = selectedQuest;

            // Save the changes to the database
            EditorUtility.SetDirty(questDatabase);
            AssetDatabase.SaveAssets();

            Debug.Log("Quest changes saved successfully.");
        } else {
            Debug.LogError("Failed to save quest changes. Quest index is out of range.");
        }
    }

    private void DeleteSelectedQuest() {
        if(selectedQuest == null) return;

        // Determine which list to delete from based on the selected dropdown
        List<Quest> currentQuestList = GetCurrentQuestList();

        if(currentQuestList != null) {
            currentQuestList.Remove(selectedQuest);
            questDatabase.RemoveQuestByID(selectedQuest.questID);
            AssetDatabase.SaveAssets();
/*            Debug.Log($"Quest '{selectedQuest.questName}' deleted.");*/
        }

        // Clear the selection
        selectedQuest = null;
        selectedQuestIndex = -1;

        // Force a refresh of the UI
        Repaint();
    }

    private List<Quest> GetCurrentQuestList() {
        switch(selectedQuestListIndex) {
            case 0: return questDatabase.allQuests;
            case 1: return questDatabase.dayQuest;
            case 2: return questDatabase.nightQuest;
            default: return null;
        }
    }
}
