using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;
    public Quest activequest;

    public bool isActive;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(Instance);
        }
    }

    public void CheckQStatue()
    {
        if (activequest.isActive && activequest.goal.HasReached())
        {
            activequest.Complete();
            activequest.rewards.AddHealth(); // MOre can be done
            return;
        }
    }

    //Seperate function for give reward which will directly give reward base on database

    private void Update()
    {
        CheckQStatue();
    }

    public void StartQuest(Quest quest)
    {
        activequest = quest;
    }


    #region Quest Requisit
    public void Killed()
    {
        if (activequest.goal.goalType == GoalType.kill)
        {
            activequest.goal.EnemyKilled();
        }
    }

    #endregion

}
