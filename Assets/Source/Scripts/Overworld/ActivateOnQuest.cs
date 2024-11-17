using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnQuest : MonoBehaviour
{
    [SerializeField] private QuestID _id;
    [SerializeField] private GameObject[] _activeWhenComplete;
    [SerializeField] private GameObject[] _activeWhenIncomplete;
    [SerializeField] private GameObject[] _unactiveWhenComplete;
    [SerializeField] private GameObject[] _unactiveWhenIncomplete;

    private void Update()
    {
        if (QuestStates.Instance.States[_id] == QuestState.Complete)
        {
            foreach (GameObject item in _activeWhenComplete)
                item.SetActive(true);
            foreach (GameObject item in _unactiveWhenComplete)
                item.SetActive(false);
        }
        else
        {
            foreach (GameObject item in _activeWhenIncomplete)
                item.SetActive(true);
            foreach (GameObject item in _unactiveWhenIncomplete)
                item.SetActive(false);
        }
    }
}
