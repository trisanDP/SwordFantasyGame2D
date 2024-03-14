using UnityEditor;
using UnityEngine;

public class QuestInputWindow : EditorWindow
{
    private int questID
    {
        get { return EditorPrefs.GetInt("QuestInputWindow.questID", 0); }
        set { EditorPrefs.SetInt("QuestInputWindow.questID", value); }
    }
    public string questName;
    public string questDes;
    public string questTask;
    public string questRewardTxt;
    public RewardType rewardType;
    public GoalType goalType;
    public Vector2 goalPosition;
    public string tag;

    public int rVal;
    public int gVal;


    private QuestDatabase questDatabase;
    private Vector2 scrollPosition;

    [MenuItem("Window/QuestInputWindow")]
    public static void ShowWindow()
    {
        GetWindow<QuestInputWindow>("QuestInputWindow");
    }

    private void OnGUI()
    {

        #region InputField

        #region QuestDetailDisplay
        //........................................................
        GUILayout.Label("Create a new Quest", EditorStyles.boldLabel);
        GUILayout.Label("Quest ID: " + questID);
        questName = EditorGUILayout.TextField("Quest Name", questName);
        questDes = EditorGUILayout.TextField("Quest Des", questDes);
        questTask = EditorGUILayout.TextField("Quest Task", questTask);
        questRewardTxt = EditorGUILayout.TextField("Quest Reward", questRewardTxt);

        #endregion

        #region Reward Selector

        rewardType = (RewardType)EditorGUILayout.EnumPopup("Reward", rewardType);
        switch (rewardType)
        {
            case RewardType.IncreaseHealth:
                rVal = EditorGUILayout.IntField("Health Amount", rVal);
                break;
            case RewardType.AddCoin:
                rVal = EditorGUILayout.IntField("Coin Amount", rVal);
                break;

        }

        #endregion

        #region Goal Selector

        goalType = (GoalType)EditorGUILayout.EnumPopup("GoalType", goalType);
        switch (goalType)
        {
            case GoalType.kill:
                gVal = EditorGUILayout.IntField("Kill Required", gVal);
                tag = EditorGUILayout.TextField("Enemy Tag:", tag);
                break;
            case GoalType.Gathering:
                gVal = EditorGUILayout.IntField("Required Amount", gVal);
                tag = EditorGUILayout.TextField("Resource Tag:", tag);
                break;
            case GoalType.GoTo:
                goalPosition = EditorGUILayout.Vector2Field("Position", goalPosition);
                break;
        }
        #endregion

        #endregion

        questDatabase = Resources.Load<QuestDatabase>("QuestDatabase");

        #region AddButton
        if (GUILayout.Button("Add Quest"))
        {
            Quest newQuest = new()
            {
                questID = this.questID,
                questName = this.questName,
                questTask = this.questTask,
                questDes = this.questDes,
                questRewardTxt = this.questRewardTxt,
/*                rewards(this.rewardType, this.val),*/
            };

            newQuest.rewards ??= new Reward(this.rewardType, this.rVal); //check if null then create new if true
            
            if (newQuest.goal == null)
            {
                switch (goalType)
                {
                    case GoalType.kill:
                        newQuest.goal = new QuestGoal(this.goalType, this.gVal,tag);
                        break;
                    case GoalType.Gathering:
                        newQuest.goal = new QuestGoal(this.goalType, this.gVal, tag);
                        break;
                    case GoalType.GoTo:
                        newQuest.goal = new QuestGoal(this.goalType, this.goalPosition);
                        break;
                }
            }

            questDatabase.quests.Add(newQuest);


            questName = "";
            questTask = "";
            questDes = "";
            questRewardTxt  = "";
            rVal = 0;
            questID++;
        }
        #endregion

        #region ClearButton

        if (GUILayout.Button("Clear All Quests"))
        {
            if (EditorUtility.DisplayDialog("Confirmation", "Are you sure you want to clear all quests?", "Yes", "No"))
            {
                questDatabase.quests.Clear();
                questID = 0;
            }
        }


        #endregion

        #region DisplayDB
        if (questDatabase != null)
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            GUILayout.Label("Quests in Database:", EditorStyles.boldLabel);
            foreach (Quest quest in questDatabase.quests)
            {
                GUILayout.Label("QuestID: " + quest.questID);
                GUILayout.Label("Quest Name: " + quest.questName);
                GUILayout.Label("Quest Des: " + quest.questDes);
                GUILayout.Label("Quest Task: " + quest.questTask);
                GUILayout.Label("Quest Reward: " + quest.questRewardTxt);
                GUILayout.Label("Quest Goal: " + quest.goal.goalType);
                GUILayout.Label("\nNext: ");
            }
            EditorGUILayout.EndScrollView();
        }
        #endregion

    }
}

