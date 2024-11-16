using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;

    public void ApplyCard(Card card)
    {
        _renderer.color = card.Color;
    }
}
