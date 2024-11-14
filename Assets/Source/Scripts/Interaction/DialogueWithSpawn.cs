using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueWithSpawn : Dialogue
{
    [SerializeField] private GameObject _whatToActivate;

    private void Start()
    {
        _whatToActivate.SetActive(false);
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (_currentDialogue == DialogueLines.Length - 1)
            _whatToActivate.SetActive(true);
        else
            _whatToActivate.SetActive(false);
    }
}
