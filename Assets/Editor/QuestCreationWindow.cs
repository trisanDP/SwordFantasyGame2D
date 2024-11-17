using NPCSystem;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class QuestCreationWindow : EditorWindow {
    #region Variables
    //-----------------------------------------------------------------------

    private string questID = "";
    private string questName = "";
    private string description = "";
    /*    private bool isRepeatable = false;*/

    private List<QuestObjective> objectives = new List<QuestObjective>();
    private List<Reward> rewards = new List<Reward>();
    /*    private List<Quest> dependentQuests = new List<Quest>();*/

    private NPCDatabase npcDatabase;
    private string[] npcNames;
    private NPCData selectedNPC;

    private QuestDatabase questDatabase;
    private string[] questNames;


    private string[] resourceTypes;

    private enum QuestGiver { NPC, System }
    /*    private QuestGiver selectedQuestGiver = QuestGiver.System;*/

    private enum QuestType { Gameplay_Day, Gameplay_Night }
    private QuestType selectedQuestType = QuestType.Gameplay_Night;



    private ResourceDatabase resourceDatabase;

    // UI foldout flags
    private bool showObjectives = true;
    private bool showRewards = true;
    private bool showDependencies = true;

    // Scroll view position
    private Vector2 scrollPosition;

    //--------------------------------------------------------
    #endregion

    [MenuItem("Quest System/Quest Creation")]
    // Functions 

    #region PreProcessing Functions
    public static void ShowWindow() {
        GetWindow<QuestCreationWindow>("Quest Creation");
    }

    private void OnEnable() {
        LoadDatabases();
        LoadResourceDatabase();
        LoadResourceTypes();
        Repaint();
    }

    #endregion

    #region LoadDataFunctions
    private void LoadResourceDatabase() {
        resourceDatabase = Resources.Load<ResourceDatabase>("ResourceDatabase");
        if(resourceDatabase == null || resourceDatabase.allResources == null || resourceDatabase.allResources.Count == 0) {
            Debug.LogWarning("Resource Database is missing or empty.");
        }
    }

    private void LoadDatabases() {
        npcDatabase = Resources.Load<NPCDatabase>("NPCDatabase");
        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");

        if(npcDatabase != null) {
            npcNames = new string[npcDatabase.npcDataList.Count];
            for(int i = 0; i < npcDatabase.npcDataList.Count; i++) {
                npcNames[i] = npcDatabase.npcDataList[i].npcName;
            }
        } else
            npcNames = new string[0];

        if(questDatabase == null) {
            // Load the quest database from the Resources folder
            questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");

            // Check if the questDatabase was successfully loaded
            if(questDatabase == null) {
                Debug.LogError("QuestDatabase could not be found. Ensure that it is located in the Resources folder.");
                return; // Prevent further code execution if the database is not loaded
            }
        }

    }

    private void LoadResourceTypes() {
        List<ResourceType> resources = Resources.Load<ResourceDatabase>("ResourceDatabase").allResources;
        if(resources != null && resources.Count > 0) {
            resourceTypes = new string[resources.Count];
            for(int i = 0; i < resources.Count; i++) {
                resourceTypes[i] = resources[i].resourceName;
            }
        } else {
            resourceTypes = new string[] { "No Resources Found" };
            Debug.LogWarning("No resource types found in Resources/ResourceTypes.");
        }
    }
    #endregion


    #region Main_GUI_Function
    private void OnGUI() {
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label("Create New Quest", EditorStyles.boldLabel);
        if(GUILayout.Button("Auto Quest", GUILayout.Height(20))) {
            AutoQuest();
        }
        if(GUILayout.Button("Reset", GUILayout.Height(20),GUILayout.Width(100))) {
            ResetFields();
        }
        DrawQuestInformation();

        DrawQuestSettingSelection();

        showObjectives = EditorGUILayout.Foldout(showObjectives, "Objectives");
        if(showObjectives) {
            DrawObjectivesSection();
        }

        showRewards = EditorGUILayout.Foldout(showRewards, "Rewards");
        if(showRewards) {
            DrawRewardsSection();
        }

        showDependencies = EditorGUILayout.Foldout(showDependencies, "Dependent Quests");
        if(showDependencies) {
            /*DrawDependentQuestsSection();*/
        }

        GUILayout.FlexibleSpace();
        if(GUILayout.Button("Create Quest", GUILayout.Height(40))) {
            CreateQuest();
        }


        EditorGUILayout.EndScrollView();
    }

    #endregion

    #region CustomFunction_GUI_Reference
    private void DrawQuestInformation() {
        DrawSectionHeader("Quest Information");
        EditorGUILayout.BeginVertical("box");
        questID = EditorGUILayout.TextField(new GUIContent("Quest ID", "Unique identifier for the quest"), questID);
        questName = EditorGUILayout.TextField(new GUIContent("Quest Name", "Name of the quest"), questName);
        description = EditorGUILayout.TextField(new GUIContent("Description", "Brief description of the quest"), description);/*
        isRepeatable = EditorGUILayout.Toggle(new GUIContent("Repeatable", "Can the quest be repeated?"), isRepeatable);*/
        EditorGUILayout.EndVertical();
    }

    private void DrawQuestSettingSelection() {
        DrawSectionHeader("Quest Settings");
        EditorGUILayout.BeginVertical("box");
        EditorGUILayout.LabelField("Select an Giver NPC", EditorStyles.boldLabel);
        if(npcDatabase != null) {
            // Create a dropdown for NPC selection
            selectedNPC = (NPCData)EditorGUILayout.ObjectField("Select NPC", selectedNPC, typeof(NPCData), false);

            // Show the selected NPC details
            if(selectedNPC != null) {
                EditorGUILayout.LabelField("Selected NPC:", selectedNPC.npcName);
            }
        }
        EditorGUILayout.Space();

        EditorGUILayout.LabelField($"QuestType", EditorStyles.boldLabel);
        selectedQuestType = (QuestType)EditorGUILayout.EnumPopup(new GUIContent("Quest Type", "Choose the quest type: ScriptableObject or Database"), selectedQuestType);
        EditorGUILayout.EndVertical();
    }

    private void DrawObjectivesSection() {
        DrawSectionHeader("Objectives");
        EditorGUILayout.BeginVertical("box");

        List<int> objectivesToRemove = new List<int>();

        for(int i = 0; i < objectives.Count; i++) {
            EditorGUILayout.BeginVertical("box");

            // Start horizontal layout for label and remove button
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Objective {i + 1}", EditorStyles.boldLabel);
            if(GUILayout.Button("Remove", GUILayout.Width(70))) {
                objectivesToRemove.Add(i);
            }
            EditorGUILayout.EndHorizontal();  // End horizontal

            // QuestObjective section
            QuestObjective objective = objectives[i];
            objective.description = EditorGUILayout.TextField("Description", objective.description);
            objective.type = (QuestObjective.ObjectiveType)EditorGUILayout.EnumPopup("Objective Type", objective.type);

            // Collect/Deliver types
            if(objective.type == QuestObjective.ObjectiveType.Collect || objective.type == QuestObjective.ObjectiveType.Deliver) {
                objective.resourceType = (ResourceType)EditorGUILayout.ObjectField("Resource Type", objective.resourceType, typeof(ResourceType), false);
                objective.targetAmount = EditorGUILayout.IntField("Amount", objective.targetAmount);
            }

            // NPC selection for Deliver type
            if(objective.type == QuestObjective.ObjectiveType.Deliver) {
                // Ensure correct casting
                objective.npcData = (NPCData)EditorGUILayout.ObjectField("NPC", objective.npcData, typeof(NPCData), true);
            }

            EditorGUILayout.EndVertical();  // End vertical layout for this objective
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();  // End main vertical layout

        // Handle removal of objectives
        foreach(int index in objectivesToRemove) {
            objectives.RemoveAt(index);
        }

        // Button to add a new objective
        if(GUILayout.Button("Add Objective", GUILayout.Width(150))) {
            objectives.Add(new QuestObjective());
        }
    }


    private void DrawRewardsSection() {
        DrawSectionHeader("Rewards");
        EditorGUILayout.BeginVertical("box");

        List<int> rewardsToRemove = new List<int>();

        for(int i = 0; i < rewards.Count; i++) {
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Reward {i + 1}", EditorStyles.boldLabel);
            if(GUILayout.Button("Remove", GUILayout.Width(70))) {
                rewardsToRemove.Add(i);
            }
            EditorGUILayout.EndHorizontal();

            Reward reward = rewards[i];
            reward.rewardType = (RewardType)EditorGUILayout.EnumPopup("Reward Type", reward.rewardType);

            if(reward.rewardType == RewardType.Resource) {
                reward.resourceType = (ResourceType)EditorGUILayout.ObjectField("Resource Type", reward.resourceType, typeof(ResourceType), false);
                reward.amount = EditorGUILayout.IntField("Amount", reward.amount);
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();
        }

        EditorGUILayout.EndVertical();

        foreach(int index in rewardsToRemove) {
            rewards.RemoveAt(index);
        }

        if(GUILayout.Button("Add Reward", GUILayout.Width(150))) {
            rewards.Add(new Reward());
        }
    }

    private void DrawSectionHeader(string sectionTitle) {
        EditorGUILayout.Space();
        GUILayout.Label(sectionTitle, EditorStyles.boldLabel);
        EditorGUILayout.Space();
    }

    #endregion

    #region Create Function
    private void AutoQuest() {
        Quest newQuest = new Quest();
        questID = "001";
        questName = "Auto Quest";
        description = "Auto Quest Description";
        selectedQuestType = QuestType.Gameplay_Day;
        /*        newQuest.objectives = new List<QuestObjective>(objectives);
                newQuest.rewards = new List<Reward>(rewards);*/
        selectedNPC = npcDatabase.npcDataList[0];
        if(objectives.Count == 0) {
            QuestObjective newObjective = new QuestObjective();
            newObjective.type = QuestObjective.ObjectiveType.Deliver;
            newObjective.npcData = npcDatabase.npcDataList[0];
            newObjective.resourceType = resourceDatabase.allResources[0];
            newObjective.targetAmount = 10;
            objectives.Add(newObjective);
        }
        if(rewards.Count == 0) { 
            Reward reward = new();
            reward.rewardType = RewardType.Resource;
            reward.resourceType = resourceDatabase.allResources[4];
            reward.amount = 10;
            rewards.Add(reward);
        }

    }
    private void CreateQuest() {
        if(string.IsNullOrEmpty(questName) || string.IsNullOrEmpty(questID)) {
            Debug.LogError("Quest ID or Name is missing. Please fill both fields.");
            return;
        }

        Quest newQuest = new Quest();
        if(questDatabase.GetQuestByID(questID) != null) {
            Debug.LogError("Quest ID already exists.");
            return;
        }

        foreach(Quest quest in questDatabase.allQuests) {
            if(quest.questID == questID) {
                Debug.LogError("Quest ID already exists.");
                return;
            } 
        }
        newQuest.questID = questID;
        newQuest.questName = questName;
        newQuest.description = description;
        newQuest.objectives = new List<QuestObjective>(objectives);
        newQuest.rewards = new List<Reward>(rewards);
        newQuest.questGiver = selectedNPC;
/*        Debug.Log(newQuest.objectives[0].npcData.name);
        Debug.Log(newQuest.objectives[0].npcData.npcName);*/

        if(selectedQuestType == QuestType.Gameplay_Day) {
            SaveInDayQuestList(newQuest);
        } else if(selectedQuestType == QuestType.Gameplay_Night) {
            SaveInNightQuestList(newQuest);
        }
        SaveAsDataInDatabase(newQuest);
        // Reset fields after creating the quest
        ResetFields();
    }


    void SaveInDayQuestList(Quest quest) {
        questDatabase.dayQuest.Add(quest);

    }

    void SaveInNightQuestList(Quest quest) {
        questDatabase.dayQuest.Add(quest);
    }

    private void SaveAsDataInDatabase(Quest quest) {
        // Save the new quest in the Quest Database located in the Resources folder
        if(questDatabase == null) {
            Debug.LogError("QuestDatabase is not loaded.");
            return;
        }

        questDatabase.AddQuest(quest);
        EditorUtility.SetDirty(questDatabase); // Mark as dirty to ensure changes are saved
        AssetDatabase.SaveAssets();
/*        Debug.Log($"Quest '{questName}' added to Quest Database.");*/
    }
    private void ResetFields() {
        questID = "";
        questName = "";
        description = "";
        selectedNPC = null;
        /*        isRepeatable = false;*/
        /*        selectedQuestGiver = QuestGiver.System; */
        objectives.Clear();
        rewards.Clear();
        /*        dependentQuests.Clear();*/
    }
    #endregion

}

