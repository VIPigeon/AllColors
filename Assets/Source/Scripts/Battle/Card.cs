using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Pokemon Card", fileName = "Pokemon_Card")]
public class Card : ScriptableObject {
    // Внимание! У некоторых карт (например, у крови) покемона нет.
    // В этом случае тут будет null 🐦
    public Pokemon Pokemon;
    public Color Color = Color.white;
    public string Name = "Без названия";
}