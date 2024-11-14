using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CardView : MonoBehaviour {
    public TextMeshProUGUI Text;
    public event Action<CardView> Clicked;

    // Вообще это плохо. То есть должен быть чувак сверху,
    // который смотрит за вьюхами и за моделями и оркестрирует
    // ивентами. Но мне лень его писать, скоро спать надо. И
    // вообще, ООП отстой.
    //
    // Привет, это kawaii-Code из будущего, в 23:30 🤟🏻. Я добавил
    // чувака сверху и всё стало норм. Правда, ООП все ещё отстой.
    public Card CardThatWeCurrentlyDisplay;

    public void Show(Card card) {
        Text.color = card.Color;
        Text.text = card.Name;
        CardThatWeCurrentlyDisplay = card;
    }

    public void OnClick() {
        Clicked?.Invoke(this);
    }
}