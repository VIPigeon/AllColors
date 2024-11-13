using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour {
    public TextMeshProUGUI Text;

    // Внимание! У некоторых карт (например, у крови) покемона нет.
    // В этом случае тут будет null 🐦
    public Pokemon Pokemon;
    public Color Color;
    public string Name;

    public event Action<Card> Clicked;

    public void Start() {
        Text.color = Color;
        Text.text = Name;
    }

    public void OnClick() {
        Clicked?.Invoke(this);
    }
}