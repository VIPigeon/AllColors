using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _interactionRange;

    public Action<bool, string> DialogueInteraction;

    private void OnEnable()
    {
        InputInitializer.Instance.InteractInput += OnIntreractInput;
    }

    private void OnDisable()
    {
        InputInitializer.Instance.InteractInput -= OnIntreractInput;
    }

    private void OnIntreractInput()
    {
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(transform.position, _interactionRange))
        {
            if(collider.TryGetComponent(out Dialogue dialogue))
            {
                DoDialogueInteraction(dialogue);
            }
        }
    }

    private void DoDialogueInteraction(Dialogue dialogue)
    {
        dialogue.OnInteract();
        DialogueInteraction?.Invoke(dialogue.InDialogue, dialogue.GetCurrentDialogue());
    }
}
