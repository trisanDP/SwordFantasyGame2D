using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;


[System.Serializable]
public class QuestGoal
{

    
    public GoalType goalType;
    public int required;
    public int current;    
    public Vector2 goalPosition;
    public string Notetag;

    
    public QuestGoal(GoalType goalTyp,int req, string tag)
    {
        //Kill Goal && collect goal
        goalType = goalTyp;
        required = req;
        Notetag = tag;
    }
    
    public QuestGoal(GoalType gType,Vector2 pos) {
        // Go To goal
        goalType = gType;
        goalPosition = pos;
    
    }

    public bool HasReached()
    {
        return (current >= required);
    }

    public void EnemyKilled()
    {
        current++;
    }
    
    

}

public enum GoalType
{
    kill,
    Gathering,
    GoTo,
    Doing
}