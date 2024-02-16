using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class QuestGiver : MonoBehaviour
{
    private Quest quest;
    public GameObject playerObj;
    P_QuestManager playerQM;

    public GameObject questWindow;
    public TMP_Text qTitle;
    public TMP_Text qDetail;
    public TMP_Text qTask;
    public TMP_Text qReward;


    private void Start()
    {
        playerObj = GameObject.Find("Player");
        playerQM = playerObj.GetComponent<P_QuestManager>();
    }

    public void ActivateQuest(Quest q)
    {
        quest = q;
        q.hasLaunched = true;
        OpenQuestWindow();
    }

    public void OpenQuestWindow(){
        questWindow.SetActive(true);
        qTitle.text = quest.questName;
        qDetail.text = quest.questDes;
        qTask.text = quest.questTask;
        qReward.text = quest.questRewardTxt;
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.Started();
        QuestManager.Instance.StartQuest(quest);
    }

    public void Reject()
    {
        questWindow.SetActive(false);
        quest.Rejected();
    }
}
