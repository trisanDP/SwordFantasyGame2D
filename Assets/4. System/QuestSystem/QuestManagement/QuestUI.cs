using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestUI : MonoBehaviour
{
    public List<Quest> quest;
    public GameObject playerObj;
    P_QuestManager player;

    public GameObject questWindow;
    public TMP_Text qTitle;
    public TMP_Text qDetail;
    public TMP_Text qTask;
    public TMP_Text qReward;


    //Handles Quest Accept, Reject and Display on Screen
    private void Start()
    {
        playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<P_QuestManager>();
    }

    public void OpenQuestWindow(int Q_Id){
        Debug.Log(Q_Id);
        questWindow.SetActive(true);
        qTitle.text = quest[Q_Id].questName; 
        qTask.text = quest[Q_Id].questTask;
        qReward.text = quest[Q_Id].questRewardTxt;
    }

    public void AcceptQuest(int Q_Id)
    {
        Debug.Log(Q_Id);
        questWindow.SetActive(false);
        quest[Q_Id].isActive = true;
        player.quest[Q_Id] = quest[Q_Id];
    }

    public void Reset()
    {
        questWindow.SetActive(false);
    }
}
