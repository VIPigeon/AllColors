using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueGiveCard : DialogueInteraction
{
    [SerializeField] private CardConfig _whatToGive;
    [SerializeField] private bool _checkIfDeckEmpty;

    public override void OnInteract()
    {
        base.OnInteract();
        if (_currentDialogue == DialogueLines.Length - 1)
        {
            if (_checkIfDeckEmpty)
                foreach (CardConfig card in FullDeck.Instance.Cards)
                    if (card.Type != CardType.Invalid)
                        return;
            FullDeck.Instance.Cards.Add(_whatToGive);
            FindObjectOfType<DeckUI>(true).AddCardToGrid(_whatToGive);
        }
    }
}
