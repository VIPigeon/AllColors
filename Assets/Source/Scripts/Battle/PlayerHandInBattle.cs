using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandInBattle : MonoBehaviour {
    public const int CardsThatPlayerHasInHand = 5;
    
    // Вьюшка (👀) смешалась с моделью (🤖). А что делать? А вы как думаете?
    public List<CardView> CardViews;
    
    public HandAndDeckOfCards Hand;
    public bool OutOfCards { get; private set; }
    
    public event Action<Card> CardPlayed;

    public void Construct(FullDeck fullDeck) {
        Hand = new HandAndDeckOfCards(fullDeck, CardsThatPlayerHasInHand);
        ShowCards();
    }
    
    // ... причина 72 почему мне не нравятся ивенты:
    private void OnEnable() {
        foreach (CardView cardView in CardViews) {
            cardView.Played += OnCardViewPlayed;
        }
    }
    
    private void OnDisable() {
        foreach (CardView cardView in CardViews) {
            cardView.Played -= OnCardViewPlayed;
        }
    }
    
    public void ReturnCard(Card card) {
        int insertionIndex = -1;
        for (int i = 0; i < Hand.MaxCardsInHand; i++) {
            if (Hand.Hand[i] == null) {
                insertionIndex = i;
                break;
            }
        }
        if (insertionIndex == -1) {
            Debug.LogError("Супер плохо. Обратитесь к kawaii-Code");
            return;
        }
        Hand.Hand[insertionIndex] = card;
        ShowCards();
    }
    
    public void Refill() {
        Hand.RefillHand();
        ShowCards();
    }
    
    private void OnCardViewPlayed(CardView cardView) {
        // Я реально не знаю, кто это будет читать и зачем я пишу комментарии. Со всех
        // сторон мне говорят что комментарии бесполезны, они засоряют код и быстро устаревают.
        // Говорят, что это трата времени: удел джунов и студентов/школьников, которых заставляют
        // комментировать код на занятиях (👀). Все вокруг говорят, что я неправ. Что мне стоит
        // прекратить писать комментарии и заняться чем-то полезным. Я не согласен! ✊
        //
        // Комментарии разбавляют сухой, безмерно строгий, бюрократический стиль, в котором пишется
        // код. Они напонимают, что в этом файле когда-то ступал человек. Комментарий - небольшой снимок
        // истории, возможность прикоснуться к прошлому. Я всегда представляю, как через много лет инопланетяне
        // найдут этот код и будут читать мои комментарии. Ради этого я их и пишу. Ну, чтобы инопланетяне
        // читали. 👽  Да... Вот так.
        //
        // Кстати, вот устаревший комментарий:
        // 
        // > Должно быть не здесь, но уже 22 часа ночи, а я в поезде второй час
        // > программирую (или 3-ий), короче немного устал, сами потом почините
        // > или подумайте как это сделать лучше 😜
        // > 
        // > Уже 23 часа ночи, и я это починил. Оставлю здесь ради истории.
        // > 
        // > Сейчас полночь, что-то мне не очень нравится передавать сюда вьюшку,
        // > но с другой стороны мне пофиг. Мы же на джем игру делаем. Да и вообще,
        // > хочу процедурный код писать. Эххх... 😛
        
        // 🤓😭 Virgin -- Используй CardViews.Find()!!! Так код будет короче и читаемей!! Или LINQ
        // 😎🕶 Gigachad -- for
        int indexOfPlayedCard = -1;
        for (int i = 0; i < Hand.MaxCardsInHand; i++) {
            if (cardView == CardViews[i]) {
                indexOfPlayedCard = i;
            }
        }
        
        // У меня какая-то чудовищная неприязнь к ивентам. Вот когда я пишу процедурно,
        // весь этот трэш не нужен. А как только ступаю своим кроссовком в грязное болото 🦢
        // ООП, так сразу вылезает эта хрень.
        Card card = Hand.PlayCard(indexOfPlayedCard);
        CardViews[indexOfPlayedCard].InteractionsDisabled = true;
        
        CardPlayed?.Invoke(card);
        ShowCards();
    }
    
    private void ShowCards() {
        OutOfCards = true;
        
        for (int i = 0; i < Hand.MaxCardsInHand; i++) {
            if (Hand.Hand[i] != null) {
                OutOfCards = false;
                CardViews[i].InteractionsDisabled = false;   
            }
        }
        
        for (int i = 0; i < Hand.MaxCardsInHand; i++) {
            CardViews[i].Show(Hand.Hand[i]);
        }
    }
}