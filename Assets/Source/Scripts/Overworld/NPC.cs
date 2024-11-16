using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField] private CharacterID _id;
    [SerializeField] private GameObject _normalState;
    [SerializeField] private GameObject _defeatedState;

    private void Start()
    {
        if (!NPCState.Instance.States.ContainsKey(_id))
            NPCState.Instance.States.Add(_id, CharacterState.Normal);
        ApplyState();
    }

    private void ApplyState()
    {
        _normalState.SetActive(false);
        _defeatedState.SetActive(false);

        switch (NPCState.Instance.States[_id])
        {
            case CharacterState.Normal:
                _normalState.SetActive(true);
                break;
            case CharacterState.Defeated:
                _defeatedState.SetActive(true);
                break;
        }
    }
}