using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public TextMeshProUGUI Text;

    public Color Color;
    public string Name;

    public void Start() {
        Text.color = Color;
        Text.text = Name;
    }
}