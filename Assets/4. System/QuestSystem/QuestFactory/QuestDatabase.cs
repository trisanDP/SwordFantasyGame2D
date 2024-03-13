using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDatabase", menuName = "Quests/QuestDatabase", order = 1)]
public class QuestDatabase : ScriptableObject
{
    public List<Quest> quests = new();
    [HideInInspector]public List<Quest> AcceptedQuests = new();
    [HideInInspector]public List<Quest> RejectedQuests = new();

}
