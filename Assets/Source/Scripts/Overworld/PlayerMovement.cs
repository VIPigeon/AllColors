using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerInteraction _interaction;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private Sprite _spriteUp;
    [SerializeField] private Sprite _spriteDown;
    [SerializeField] private Sprite _spriteRight;
    [SerializeField] private Sprite _spriteLeft;

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

        if (moveVector.y > 0)
            _sprite.sprite = _spriteUp;
        else
            _sprite.sprite = _spriteDown;

        if (moveVector.x > 0)
            _sprite.sprite = _spriteRight;
        else if (moveVector.x < 0)
            _sprite.sprite = _spriteLeft;

    }

    private void OnDialogueInteraction(bool state, DialogueLine dialogue)
    {
        _canMove = !state;
    }
}
