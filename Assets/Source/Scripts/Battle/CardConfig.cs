using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Pokemon Card", fileName = "Pokemon_Card")]
public class CardConfig : ScriptableObject {
    // Внимание! У заклинаний (например, у крови) покемона нет.
    // В этом случае тут будет null 🐦
    public Pokemon Pokemon;
    public CardType Type;
    public string Description = "";
    public Color Color = Color.white;
    public ColorType ColorType = ColorType.Red;
    public string Name = "Без названия";
    
    [Range(1, 100)]
    public int InitialHealth = 3;
    [Range(0, 30)]
    public int InitialDamage = 2;
}

public enum ColorType {
    Red,
    Blue,
    Yellow,
    Purple,
    Green,
    Orange,
}

public class ColorInfo {
    public static Dictionary<ColorType, Dictionary<ColorType, double>> DamageBonuses = new() {
        {
            ColorType.Red,
            new Dictionary<ColorType, double> {
                {ColorType.Red, 0.5},
                {ColorType.Blue, 1.25},
                {ColorType.Yellow, 1.25},
                {ColorType.Purple, 1},
                {ColorType.Green, 1.5},
                {ColorType.Orange, 1},
            }
        },
        {
            ColorType.Blue,
            new Dictionary<ColorType, double> {
                {ColorType.Red, 1.25},
                {ColorType.Blue, 0.5},
                {ColorType.Yellow, 1.25},
                {ColorType.Purple, 1},
                {ColorType.Green, 1},
                {ColorType.Orange, 1.5},
            }
        },
           {
            ColorType.Yellow,
            new Dictionary<ColorType, double> {
                {ColorType.Red, 1.25},
                {ColorType.Blue, 1.25},
                {ColorType.Yellow, 0.5},
                {ColorType.Purple, 1.5},
                {ColorType.Green, 1},
                {ColorType.Orange, 1},
            }
        },
           {
            ColorType.Purple,
            new Dictionary<ColorType, double> {
                {ColorType.Red, 1},
                {ColorType.Blue, 1},
                {ColorType.Yellow, 1.5},
                {ColorType.Purple, 0.5},
                {ColorType.Green, 1.25},
                {ColorType.Orange, 1.25},
            }
        },
           {
            ColorType.Green,
            new Dictionary<ColorType, double> {
                {ColorType.Red, 1.5},
                {ColorType.Blue, 1},
                {ColorType.Yellow, 1},
                {ColorType.Purple, 1.25},
                {ColorType.Green, 0.5},
                {ColorType.Orange, 1.25},
            }
        },
           {
            ColorType.Orange,
            new Dictionary<ColorType, double> {
                {ColorType.Red, 1},
                {ColorType.Blue, 1.5},
                {ColorType.Yellow, 1},
                {ColorType.Purple, 1.25},
                {ColorType.Green, 1.25},
                {ColorType.Orange, 0.5},
            }
        },
    };
}