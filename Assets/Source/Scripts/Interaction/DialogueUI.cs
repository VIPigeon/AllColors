using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _interaction;
    [SerializeField] private GameObject _panel;
    [SerializeField] private TMP_Text _dialogueText;

    private void OnEnable()
    {
        _panel.SetActive(false);
        //TODO Unsubscribe from events
        _interaction.DialogueInteraction += DrawDialoguePanel;
    }

    private void DrawDialoguePanel(bool state, string text)
    {
        _panel.SetActive(state);
        _dialogueText.text = text;
    }
}
