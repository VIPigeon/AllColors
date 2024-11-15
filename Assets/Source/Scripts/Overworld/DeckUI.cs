using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckUI : MonoBehaviour
{
    [SerializeField] private GameObject _cardPrefab;
    [SerializeField] private Transform _deckGrid;
    private FullDeck _deck;

    private void Start()
    {
        _deck = FullDeck.Instance;
        foreach (Card card in _deck.Cards)
            AddCardToGrid(card);
    }

    private void AddCardToGrid(Card card)
    {
        GameObject cardInstance = Instantiate(_cardPrefab);
        cardInstance.transform.SetParent(_deckGrid);
        cardInstance.GetComponent<Image>().color = card.Color;
    }
}
