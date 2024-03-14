using System.Collections.Generic;
using UnityEngine;


//Gives Player His Current Active Quest and all Accepted Quests which he can choose at will
public class P_QuestManager : MonoBehaviour
{

    public QuestUI questUi;
    public List<Quest> quest;

    public Quest activeQuest;



    public void SetAsActiveQuest(int Q_Id) {
        activeQuest = quest[Q_Id];  
    }

    public void HasRejected(Quest quest)
    {
        
    }
}
