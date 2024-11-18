using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStates : Singleton<QuestStates>
{
    public Dictionary<QuestID, QuestState> States;
    public Vector3 PlayerPositionSave;

    private void OnEnable()
    {
        PlayerPositionSave = FindObjectOfType<PlayerMovement>().transform.position;
        if (States == null)
        {
            States = new Dictionary<QuestID, QuestState>();

            foreach (QuestID quest in Enum.GetValues(typeof(QuestID)))
            {
                States[quest] = QuestState.Incomplete;
            }
        }
    }
}


public enum QuestID
{
    Squirrel,
    GetBasicDeck,
}

public enum QuestState
{
    Incomplete,
    Complete
}