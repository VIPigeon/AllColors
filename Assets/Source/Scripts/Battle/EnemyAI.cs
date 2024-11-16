using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
    public List<EnemyCardView> CardViews;
    public FullDeck FullDeck;
    
    public HandAndDeckOfCards Hand;
    public bool OutOfCards { get; private set; }
    
    private void Start() {
        Hand = new HandAndDeckOfCards(FullDeck, FullDeck.Cards.Count);
    }
    
    public EnemyTurn DoTurn(Pokemon myPokemon, Pokemon otherPokemon) {
        if (myPokemon == null || myPokemon.Card.CurrentHealth.IsZero) {
            Card card = Hand.PlayRandomCard();
            if (card == null) {
                return new EnemyTurn() {
                    Type = EnemyTurnType.GiveUp,
                };
            }
            return new EnemyTurn() {
                Type = EnemyTurnType.PlayCard,
                Card = card,
            };
        } else {
            return new EnemyTurn() {
                Type = EnemyTurnType.Attack,
            };
        }
        
        ShowCards();
    }
    
    public void ReturnCard(Card card) {
        Debug.LogError("Не сделано");
        ShowCards();
    }
    
    private void ShowCards() {
        OutOfCards = true;
        
        for (int i = 0; i < FullDeck.Cards.Count; i++) {
            if (Hand.Hand[i] == null) {
                CardViews[i].Disappear();
            } else {
                OutOfCards = false;
            }
        }
    }
}
