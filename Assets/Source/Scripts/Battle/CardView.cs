using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardView : MonoBehaviour {
    public TextMeshProUGUI Text;
    public GameObject CardObject;
    public event Action<CardView> Played;
    public bool InteractionsDisabled;
    
    // Вообще это плохо. То есть должен быть чувак сверху,
    // который смотрит за вьюхами и за моделями и оркестрирует
    // ивентами. Но мне лень его писать, скоро спать надо. И
    // вообще, ООП отстой.
    //
    // Привет, это kawaii-Code из будущего, в 23:30 🤟🏻. Я добавил
    // чувака сверху и всё стало норм. Правда, ООП все ещё отстой.
    public Card CardThatWeCurrentlyDisplay;

    // card может быть null. А как же иначе?
    public void Show(Card card) {
        if (card == null) {
            CardObject.SetActive(false);
            return;
        }
        
        CardObject.SetActive(true);
        Text.color = card.Config.Color;
        Text.text = card.Config.Name;
        CardThatWeCurrentlyDisplay = card;
    }
    
    public void OnClick() {
        if (InteractionsDisabled) {
            return;
        }
        Played?.Invoke(this);
    }
}