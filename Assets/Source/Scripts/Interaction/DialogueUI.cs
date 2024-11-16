using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private PlayerInteraction _interaction;
    [SerializeField] private GameObject _panel;
    [SerializeField] private GameObject _deck;
    [SerializeField] private WobblyText _wobbleEffect;
    [SerializeField] private TMP_Text _dialogueText;

    private void OnEnable()
    {
        _panel.SetActive(false);
        //TODO Unsubscribe from events
        _interaction.DialogueInteraction += DrawDialoguePanel;
    }

    private void DrawDialoguePanel(bool state, DialogueLine dialogue)
    {
        _panel.SetActive(state);
        _deck.SetActive(!state);
        _dialogueText.text = dialogue.Text;
        _dialogueText.color = dialogue.TextColor;
        _wobbleEffect.enabled = dialogue.IsWobbly;
    }
}
