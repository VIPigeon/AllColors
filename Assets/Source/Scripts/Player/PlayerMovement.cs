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
        //TODO Unsubscribe from events
    }

    private void OnDisable()
    {
        if(!Singleton.Quitting)
            InputInitializer.Instance.MovementInput -= OnMovementInput;
    }

    private void OnMovementInput(Vector2 moveVector)
    {
        //TODO Change velocity in FixedUpdate
        if(_canMove)
            _rigidbody.velocity = moveVector.normalized * _speed;
    }

    private void OnDialogueInteraction(bool state, string text)
    {
        _canMove = !state;
    }
}
