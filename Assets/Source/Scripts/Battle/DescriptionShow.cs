using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DescriptionShow : MonoBehaviour
{
    public TMP_Text Description;

    public List<CardView> descriptionOnEvent;
    public List<CardView> descriptionOffEvent;

    // private Dictionary<string, int> name_2_index = new Dictionary<string, int>
    // {
    //     {""},
    // };

    void Start()
    {
        // Debug.Log("start");
        foreach (CardView ev in descriptionOnEvent)
        {
            if (ev != null) {
                ev.DescriptionOn += ShowDescription;
                // Debug.Log("subscribe");
            }
        }
        foreach (CardView ev in descriptionOnEvent)
        {
            if (ev != null) {
                ev.DescriptionOff += HideDescription;
            }
        }
    }

    void ShowDescription(string text)
    {
        // Debug.Log(text);
        // Description.gameObject.SetActive(true);
        Description.text = text;
        // Descriptions[name].SetActive(true);
    }

    void HideDescription()
    {
        // Debug.Log("hide");
        // Descriptions[0].gameObject.SetActive(false);
        Description.text = "";
        // Descriptions[name].SetActive(false);
    }

    void OnDestroy()
    {
        foreach (CardView ev in descriptionOnEvent)
        {
            if (ev != null) {
                ev.DescriptionOn -= ShowDescription;
            }
        }
        foreach (CardView ev in descriptionOnEvent)
        {
            if (ev != null) {
                ev.DescriptionOff -= HideDescription;
            }
        }
    }
}
