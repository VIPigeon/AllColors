using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerDeck : MonoBehaviour
{
    public List<CardConfig> Cards;

    public CardConfig ClosestCardToColor(Color color)
    {
        float minColorDistance = float.PositiveInfinity;
        CardConfig result = null;
        foreach(CardConfig card in Cards)
        {
            if(minColorDistance >= ColorDistance(card.Color, color))
            {
                minColorDistance = ColorDistance(card.Color, color);
                result = card;
            }
        }
        return result;
    }

    public float ColorDistance(Color color1, Color color2)
    {
        return (Mathf.Abs(Mathf.Pow((color1.r - color2.r),2)) +
            Mathf.Abs(Mathf.Pow((color1.g - color2.g), 2)) +
            Mathf.Abs(Mathf.Pow((color1.b - color2.b), 2)));
    }
}
