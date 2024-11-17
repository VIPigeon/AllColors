using UnityEngine;
using System.Collections.Generic;

public class EnemyAI : MonoBehaviour {
    public List<EnemyCardView> CardViews;
    public FullDeckForOthers FullDeck;
    public CharacterID CharacterID;
    
    public HandAndDeckOfCards Hand;
    public bool OutOfCards { get; private set; }
    
    private void Awake() {
        Hand = new HandAndDeckOfCards(FullDeck, FullDeck.Cards.Count);
    }
    
    public Card FirstTurn() {
        int pokemonIndex = -1;
        for (int i = 0; i < Hand.Hand.Count; i++) {
            if (Hand.Hand[i].Config.Type.IsPokemon()) {
                if (pokemonIndex != -1) {
                    Debug.LogError("Тревога! У врага должно быть не больше одного покемона в колоде!");
                    return null;
                }
                pokemonIndex = i;
            }
        }
        
        if (pokemonIndex == -1) {
            Debug.LogError("Тревога! У врага должен быть покемон в колоде. Это костыль, но так уж вышло!");
            return null;
        }
        
        return Hand.PlayCard(pokemonIndex);
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
