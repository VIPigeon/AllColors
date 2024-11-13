using UnityEngine;
using System.Collections.Generic;

public class Hand : MonoBehaviour {
    // Вьюшка (👀) смешалась с моделью (🤖). А что делать? А вы как думаете?
    public List<CardView> CardViews;

    public const int CountCardsInHand = 5;
    public List<Card> Cards;
    public List<Card> Deck;
    public Card DefaultCard;

    public void FillFrom(FullDeck fullDeck) {
        foreach (Card card in fullDeck.Cards) {
            Deck.Add(card);
        }

        Shuffle(Deck);

        for (int i = 0; i < CountCardsInHand; i++) {
            Cards.Add(DrawOneCardFromDeck());
        }

        ShowCards();
    }

    public void PlayCard(CardView cardView) {
        if (Cards.Count != CountCardsInHand) {
            Debug.LogError("Что-то не так, карт меньше, чем должно быть.");
            return;
        }

        ReplaceCardFromHandWithCardFromDeck(cardView);
        ShowCards();
    }

    private void ReplaceCardFromHandWithCardFromDeck(CardView cardView) {
        int indexOfCardInHand = -1;
        for (int i = 0; i < CountCardsInHand; i++) {
            if (cardView == CardViews[i]) {
                indexOfCardInHand = i;
            }
        }

        if (indexOfCardInHand == -1) {
            Debug.LogError("Была сыграна карта, которой нету в руке игрока. Так нельзя.");
            return;
        }

        // Virgin   🤓😭 -- Асимпотика O(n)!!! Есть более эффективные структуры данных!
        // Gigachad 😎🕶 -- Константа маленькая
        Cards.RemoveAt(indexOfCardInHand);
        Cards.Insert(indexOfCardInHand, DrawOneCardFromDeck());
    }

    private Card DrawOneCardFromDeck() {
        if (Deck.Count > 0) {
            Card card = Deck[Deck.Count - 1];
            Deck.RemoveAt(Deck.Count - 1);
            return card;
        } else {
            return DefaultCard;
        }
    }

    private void ShowCards() {
        for (int i = 0; i < CountCardsInHand; i++) {
            CardViews[i].Show(Cards[i]);
        }
    }

    // Это мой любимый алгоритм перемешки 🥊
    private void Shuffle(List<Card> cards) {
        for (int i = 0; i < cards.Count; i++) {
            int j = Random.Range(i, cards.Count);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }
}