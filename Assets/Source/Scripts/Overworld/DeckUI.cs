using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private GameObject _colorPickerPrefab;
    [SerializeField] private Transform _deckGrid;
    private FullDeck _deck;

    private void Start()
    {
        _deck = FullDeck.Instance;
        foreach (Card card in _deck.Cards)
            AddCardToGrid(card);
    }

    public void AddCardToGrid(Card card)
    {
        GameObject cardInstance;
        if(card.name == "ColorPicker")
            cardInstance = Instantiate(_colorPickerPrefab);
        else
            cardInstance = Instantiate(_cardPrefab);

        cardInstance.transform.SetParent(_deckGrid);

        cardInstance.GetComponent<DraggableCard>().SetCard(card);
    }
}
