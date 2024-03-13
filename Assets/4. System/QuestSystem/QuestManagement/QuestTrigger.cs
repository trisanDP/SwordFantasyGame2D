using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    QuestGiver questGiver;
    QuestDatabase questDb;

    private void Start()
    {
        questGiver = GameObject.Find("QuestGiverUI").GetComponent<QuestGiver>();
        questDb = Resources.Load<QuestDatabase>("QuestDatabase");
        if(questDb == null)
        {
            Debug.Log("Null");
            
        }
    }
    private void Update()
    {
        if (questDb.quests == null)
        {
            Debug.Log("No Quest in DB");
        } else
        {
            if (Input.GetKeyDown(KeyCode.Q) && questDb.quests[0].hasLaunched != true)
            {
                questGiver.ActivateQuest(questDb.quests[0]);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                questGiver.ActivateQuest(questDb.quests[1]);
            }
        }
    }

/*    void StartQuest(int questID)
    {
        *//*questGiver.OpenQuestWindow();*//*
        
    }
*/
}
    
