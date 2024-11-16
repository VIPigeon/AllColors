using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStates : Singleton<QuestStates>
{
    public Dictionary<QuestID, QuestState> States;

    private void OnEnable()
    {
        if (States == null)
            States = new Dictionary<QuestID, QuestState>();
    }
}


public enum QuestID
{
    Squirrel,
}

public enum QuestState
{
    Incomplete,
    Complete
}