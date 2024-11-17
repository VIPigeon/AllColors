using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogue : DialogueInteraction
{
    [SerializeField] private QuestID _id;

    public override void OnInteract()
    {
        base.OnInteract();
        if (_currentDialogue == DialogueLines.Length - 1)
        {
            if (!QuestStates.Instance.States.ContainsKey(_id))
                QuestStates.Instance.States.Add(_id, QuestState.Incomplete);
            QuestStates.Instance.States[_id] = QuestState.Complete;
        }
    }
}
