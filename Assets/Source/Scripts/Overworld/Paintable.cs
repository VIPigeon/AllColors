using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paintable : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Color _requiredColor;
    [SerializeField] private float _requiredColorThreshold;
    [SerializeField] private GameObject[] _whatToActivate;
    [SerializeField] private GameObject[] _whatToDeactivate;

    public void ApplyCard(CardConfig card)
    {
        _renderer.color = card.Color;
        if (FindObjectOfType<ColorPickerDeck>().ColorDistance(_requiredColor, card.Color) < _requiredColorThreshold)
        {
            DoEffect();
        }
    }

    protected virtual void DoEffect()
    {
        foreach (GameObject item in _whatToActivate)
            item.SetActive(true);
        foreach (GameObject item in _whatToDeactivate)
            item.SetActive(false);
    }
}
