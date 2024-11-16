using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestPaintable : Paintable
{
    [SerializeField] private QuestID _id;

    protected override void DoEffect()
    {
        base.DoEffect();
        if (!QuestStates.Instance.States.ContainsKey(_id))
            QuestStates.Instance.States.Add(_id, QuestState.Complete);

        QuestStates.Instance.States[_id] = QuestState.Complete;
    }
}
