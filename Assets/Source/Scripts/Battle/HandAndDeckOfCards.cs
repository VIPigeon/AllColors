using UnityEngine;
using System.Collections.Generic;

public class HandAndDeckOfCards {
    public List<Card> Hand;
    public List<Card> Deck;
    public int MaxCardsInHand;
    
    public HandAndDeckOfCards(FullDeck fullDeck, int maxCardsInHand) {
        Construct(fullDeck.Cards, maxCardsInHand);
    }
    
    // Кринж.. 😶 Синглтон момент (можно было чуть-чуть получше сделать, но все равно хрень была бы)
    public HandAndDeckOfCards(FullDeckForOthers fullDeck, int maxCardsInHand) {
        Construct(fullDeck.Cards, maxCardsInHand);
    }
    
    private void Construct(List<CardConfig> cards, int maxCardsInHand) {
        Deck = new List<Card>();
        Hand = new List<Card>();
        MaxCardsInHand = maxCardsInHand;
        
        foreach (CardConfig cardConfig in cards) {
            if (cardConfig.Name != "Пипетка") {
                Deck.Add(new Card(cardConfig));
            }
        }
        
        Shuffle(Deck);
        
        for (int i = 0; i < MaxCardsInHand; i++) {
            Hand.Add(DrawOneCardFromDeck());
        }
    }
    
    public void RefillHand() {
        for (int i = 0; i < MaxCardsInHand; i++) {
            if (Hand[i] == null) {
                // Можно какой-нибудь твинер сюда как анимацию
                // доставания карты
                Hand[i] = DrawOneCardFromDeck();
            }
        }
    }
    
    public Card PlayRandomCard() {
        int maxAttempts = 10;
        int attempt = 0;
        int randomIndex = -1;
        while (attempt < maxAttempts) {
            randomIndex = Random.Range(0, Hand.Count);
            if (Hand[randomIndex] != null) {
                break;
            }
            attempt += 1;
        }
        Card card = Hand[randomIndex];
        PlayCard(randomIndex);
        return card;
    }
    
    public Card PlayCard(int cardIndex) {
        if (Hand.Count != MaxCardsInHand) {
            Debug.LogError("Что-то не так, карт меньше, чем должно быть.");
            return null;
        }
        Card result = Hand[cardIndex];
        Hand[cardIndex] = null;
        return result;
    }
    
    private Card DrawOneCardFromDeck() {
        if (Deck.Count > 0) {
            Card card = Deck[Deck.Count - 1];
            Deck.RemoveAt(Deck.Count - 1);
            return card;
        } else {
            return null;
        }
    }
    
    // Это мой любимый алгоритм перемешки 🥊
    public void Shuffle(List<Card> cards) {
        for (int i = 0; i < cards.Count; i++) {
            int j = UnityEngine.Random.Range(i, cards.Count);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }
}