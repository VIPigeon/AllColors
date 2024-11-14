using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : Interactable
{
    [field: SerializeField] public DialogueLine[] DialogueLines { get; private set; }
    public bool InDialogue { get; private set; }
    protected int _currentDialogue = 0;

    private void Start()
    {
        InDialogue = false;
    }

    public override void OnInteract()
    {
        if (!InDialogue)
            InDialogue = true;
        else
        {
            _currentDialogue++;
            if (_currentDialogue >= DialogueLines.Length)
            {
                InDialogue = false;
                _currentDialogue = 0;
            }
        }
    }

    public DialogueLine GetCurrentDialogue() => DialogueLines[_currentDialogue];
}
