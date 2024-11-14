using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float _interactionRange;
    [SerializeField] private GameObject _interactionPrompt;

    public event Action<bool, string> DialogueInteraction;

    private Interactable _closestInteractable;

    private void OnEnable()
    {
        InputInitializer.Instance.InteractInput += OnIntreractInput;
    }

    private void OnDisable()
    {
        if(!Singleton.Quitting)
            InputInitializer.Instance.InteractInput -= OnIntreractInput;
    }

    private void Update()
    {
        float minInteractionDistance = _interactionRange + 1;
        _closestInteractable = null;
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(transform.position, _interactionRange))
        {
            if (collider.TryGetComponent(out Interactable interactable))
            {
                if (Vector3.Distance(collider.transform.position, transform.position) < minInteractionDistance)
                {
                    minInteractionDistance = Vector3.Distance(collider.transform.position, transform.position);
                    _closestInteractable = interactable;
                }
            }
        }

        if (_closestInteractable != null)
            ShowInteractionPrompt();
        else
            _interactionPrompt.SetActive(false);
    }

    private void ShowInteractionPrompt()
    {
        _interactionPrompt.SetActive(true);
        _interactionPrompt.transform.position = _closestInteractable.transform.position + Vector3.up;
    }

    private void OnIntreractInput()
    {
        if (_closestInteractable)
            if (_closestInteractable.TryGetComponent(out Dialogue dialogue))
                DoDialogueInteraction(dialogue);
    }

    public void ForceInteract() => OnIntreractInput();

    private void DoDialogueInteraction(Dialogue dialogue)
    {
        dialogue.OnInteract();
        DialogueInteraction?.Invoke(dialogue.InDialogue, dialogue.GetCurrentDialogue());
    }
}
