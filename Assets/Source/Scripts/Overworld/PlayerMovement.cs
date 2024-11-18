using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerInteraction _interaction;
    private Rigidbody2D _rigidbody;
    private bool _canMove = true;

    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        InputInitializer.Instance.MovementInput += OnMovementInput;
        _interaction.DialogueInteraction += OnDialogueInteraction;
    }

    private void OnDisable()
    {
        if (!Singleton.Quitting)
        {
            InputInitializer.Instance.MovementInput -= OnMovementInput;
            _interaction.DialogueInteraction -= OnDialogueInteraction;
        }
    }

    private void Start()
    {
        Debug.Log($"{QuestStates.Instance.PlayerPositionSave}");
        transform.position = QuestStates.Instance.PlayerPositionSave;
    }

    private void OnMovementInput(Vector2 moveVector)
    {
        //TODO Change velocity in FixedUpdate
        if(_canMove)
            _rigidbody.velocity = moveVector.normalized * _speed;
        else
            _rigidbody.velocity = Vector2.zero;

    }

    private void OnDialogueInteraction(bool state, DialogueLine dialogue)
    {
        _canMove = !state;
    }
}
