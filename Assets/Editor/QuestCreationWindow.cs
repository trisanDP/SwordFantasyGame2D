using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class QuestCreationWindow : EditorWindow {
    private string questID = "";
    private string questName = "";
    private string description = "";
    private bool isRepeatable = false;

    private List<QuestObjective> objectives = new List<QuestObjective>();
    private List<Reward> rewards = new List<Reward>();
    private List<Quest> dependentQuests = new List<Quest>();

    private NPCDatabase npcDatabase;
    private string[] npcNames;
    private int selectedNPC = 0;

    private QuestDatabase questDatabase;
    private string[] questNames;

    // Quest Giver Selection: NPC or System
    private enum QuestGiver { NPC, System }
    private QuestGiver selectedQuestGiver = QuestGiver.System;

    // Quest Type: ScriptableObject or Database
    private enum QuestType { ScriptableObject, Database }
    private QuestType selectedQuestType = QuestType.ScriptableObject;

    [MenuItem("Quest System/Quest Creation")]
    public static void ShowWindow() {
        GetWindow<QuestCreationWindow>("Quest Creation");
    }

    private void OnEnable() {
        LoadDatabases();
    }

    private void LoadDatabases() {
        npcDatabase = Resources.Load<NPCDatabase>("NPCDatabase");
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");

        // Initialize NPC names list
        if(npcDatabase != null && npcDatabase.npcList != null && npcDatabase.npcList.Count > 0) {
            npcNames = new string[npcDatabase.npcList.Count];
            for(int i = 0; i < npcDatabase.npcList.Count; i++) {
                npcNames[i] = npcDatabase.npcList[i].npcName;
            }
        } else {
            npcNames = new string[0];
            Debug.LogWarning("NPC Database is empty or missing.");
        }

        // Initialize quest names list
        if(questDatabase != null && questDatabase.quests != null && questDatabase.quests.Count > 0) {
            questNames = new string[questDatabase.quests.Count];
            for(int i = 0; i < questDatabase.quests.Count; i++) {
                questNames[i] = questDatabase.quests[i].questName;
            }
        } else {
            questNames = new string[0];
            Debug.LogWarning("Quest Database is empty or missing.");
        }
    }

    private void OnGUI() {
        // Title
        GUILayout.Label("Create New Quest", EditorStyles.boldLabel);
        EditorGUILayout.Space();

        // Quest Information Section
        GUILayout.Label("Quest Information", EditorStyles.helpBox);
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical("box");
        questID = EditorGUILayout.TextField(new GUIContent("Quest ID", "Unique identifier for the quest"), questID);
        questName = EditorGUILayout.TextField(new GUIContent("Quest Name", "Name of the quest"), questName);
        description = EditorGUILayout.TextField(new GUIContent("Description", "Brief description of the quest"), description);
        isRepeatable = EditorGUILayout.Toggle(new GUIContent("Repeatable", "Can the quest be repeated?"), isRepeatable);
        EditorGUILayout.EndVertical();
        GUILayout.Space(10);

        // Quest Giver Selection Section
        GUILayout.Label("Quest Giver", EditorStyles.helpBox);
        EditorGUILayout.BeginVertical("box");
        selectedQuestGiver = (QuestGiver)EditorGUILayout.EnumPopup(new GUIContent("Quest Giver", "Select who gives the quest: NPC or System"), selectedQuestGiver);

        if(selectedQuestGiver == QuestGiver.NPC) {
            if(npcNames.Length > 0) {
                selectedNPC = EditorGUILayout.Popup(new GUIContent("Select NPC", "Choose an NPC to give the quest"), selectedNPC, npcNames);
            } else {
                EditorGUILayout.LabelField("No NPCs available.", EditorStyles.miniLabel);
            }
        }
        EditorGUILayout.EndVertical();
        GUILayout.Space(10);

        // Quest Type Section
        GUILayout.Label("Quest Type", EditorStyles.helpBox);
        EditorGUILayout.BeginVertical("box");
        selectedQuestType = (QuestType)EditorGUILayout.EnumPopup(new GUIContent("Quest Type", "Choose the quest type: ScriptableObject or Database"), selectedQuestType);
        EditorGUILayout.EndVertical();
        GUILayout.Space(10);

        // Objectives Management Section
        GUILayout.Label("Objectives", EditorStyles.helpBox);
        EditorGUILayout.BeginVertical("box");
        if(GUILayout.Button(new GUIContent("Add Objective", "Add a new quest objective"), GUILayout.Width(150))) {
            objectives.Add(new QuestObjective());
        }
        RenderObjectives();
        EditorGUILayout.EndVertical();
        GUILayout.Space(10);

        // Rewards Management Section
        GUILayout.Label("Rewards", EditorStyles.helpBox);
        EditorGUILayout.BeginVertical("box");
        if(GUILayout.Button(new GUIContent("Add Reward", "Add a reward for completing the quest"), GUILayout.Width(150))) {
            rewards.Add(new Reward());
        }
        RenderRewards();
        EditorGUILayout.EndVertical();
        GUILayout.Space(10);

        // Dependent Quests Section
        GUILayout.Label("Dependent Quests", EditorStyles.helpBox);
        EditorGUILayout.BeginVertical("box");
        RenderDependentQuestsManagement();
        EditorGUILayout.EndVertical();
        GUILayout.Space(20);

        // Create Quest Button
        EditorGUILayout.Space();
        GUILayout.FlexibleSpace();
        if(GUILayout.Button("Create Quest", GUILayout.Height(40))) {
            CreateQuest();
        }
        GUILayout.FlexibleSpace();
    }


    private void RenderObjectives() {
        for(int i = 0; i < objectives.Count; i++) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Objective {i + 1}", EditorStyles.boldLabel);
            objectives[i].description = EditorGUILayout.TextField("Description", objectives[i].description);
            objectives[i].targetAmount = EditorGUILayout.IntField("Target Amount", objectives[i].targetAmount);
            objectives[i].type = (ObjectiveType)EditorGUILayout.EnumPopup("Type", objectives[i].type);

            if(GUILayout.Button("Remove Objective", GUILayout.Width(150))) {
                objectives.RemoveAt(i);
            }
            EditorGUILayout.EndVertical();
        }
    }

    private void RenderRewards() {
        for(int i = 0; i < rewards.Count; i++) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"Reward {i + 1}", EditorStyles.boldLabel);
            rewards[i].itemID = EditorGUILayout.TextField("Item ID", rewards[i].itemID);
            rewards[i].quantity = EditorGUILayout.IntField("Quantity", rewards[i].quantity);

            if(GUILayout.Button("Remove Reward", GUILayout.Width(150))) {
                rewards.RemoveAt(i);
            }
            EditorGUILayout.EndVertical();
        }
    }

    private void RenderDependentQuestsManagement() {
        if(questNames.Length == 0) {
            GUILayout.Label("No quests in the database.", EditorStyles.miniLabel);
        } else {
            if(GUILayout.Button("Add Dependent Quest")) {
                dependentQuests.Add(null);
            }

            for(int i = 0; i < dependentQuests.Count; i++) {
                EditorGUILayout.BeginVertical("box");
                int questIndex = dependentQuests[i] != null ? questDatabase.quests.IndexOf(dependentQuests[i]) : -1;
                int newQuestIndex = EditorGUILayout.Popup("Dependent Quest", questIndex, questNames);

                if(newQuestIndex >= 0 && newQuestIndex < questDatabase.quests.Count) {
                    dependentQuests[i] = questDatabase.quests[newQuestIndex];
                } else {
                    dependentQuests[i] = null;
                }

                if(GUILayout.Button("Remove Dependent Quest", GUILayout.Width(150))) {
                    dependentQuests.RemoveAt(i);
                }
                EditorGUILayout.EndVertical();
            }
        }
    }

    private void CreateQuest() {
        // Validate input
        if(string.IsNullOrEmpty(questName) || string.IsNullOrEmpty(questID)) {
            Debug.LogError("Quest ID or Name is missing. Please fill both fields.");
            return;
        }

        // Create new quest instance
        Quest newQuest = ScriptableObject.CreateInstance<Quest>();
        newQuest.questID = questID;
        newQuest.questName = questName;
        newQuest.description = description;
        newQuest.isRepeatable = isRepeatable;
        newQuest.objectives = objectives;
        newQuest.rewards = rewards;
        newQuest.dependentQuests = dependentQuests;

        // Save quest based on selected quest giver (NPC or System)
        if(selectedQuestGiver == QuestGiver.System) {
            SaveQuestToDatabase(newQuest);
        } else if(selectedQuestGiver == QuestGiver.NPC && npcDatabase != null && selectedNPC < npcDatabase.npcList.Count) {
            SaveQuestToNPC(newQuest, npcDatabase.npcList[selectedNPC]);
        }

        // Save all assets
        AssetDatabase.SaveAssets();
    }

    private void SaveQuestToDatabase(Quest newQuest) {
        if(questDatabase == null) {
            Debug.LogError("Quest Database is not found. Please ensure it is loaded.");
            return;
        }

        questDatabase.quests.Add(newQuest);
        EditorUtility.SetDirty(questDatabase);
        Debug.Log($"Quest '{questName}' added to Quest Database.");
    }

    private void SaveQuestToNPC(Quest newQuest, NPCData selectedNPC) {
        selectedNPC.quests.Add(newQuest);
        EditorUtility.SetDirty(selectedNPC);
        Debug.Log($"Quest '{questName}' added to NPC '{selectedNPC.npcName}'");
    }

    public void SetQuestToEdit(Quest quest) {
        questID = quest.questID;
        questName = quest.questName;
        description = quest.description;
        isRepeatable = quest.isRepeatable;
        objectives = quest.objectives;
        rewards = quest.rewards;
        dependentQuests = quest.dependentQuests;
    }
}
