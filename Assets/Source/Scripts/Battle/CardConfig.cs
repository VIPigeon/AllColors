using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Pokemon Card", fileName = "Pokemon_Card")]
public class CardConfig : ScriptableObject {
    // Внимание! У заклинаний (например, у крови) покемона нет.
    // В этом случае тут будет null 🐦
    public Pokemon Pokemon;
    public CardType Type;
    public string Description = "";
    public Color Color = Color.white;
    public string Name = "Без названия";
    
    [Range(1, 30)]
    public int InitialHealth = 3;
    [Range(0, 30)]
    public int InitialDamage = 2;
}
