using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    public delegate void DescriptionOnHandler(string text);
    public event DescriptionOnHandler DescriptionOn;

    public delegate void DescriptionOffHandler();
    public event DescriptionOffHandler DescriptionOff;

    public HealthView Healthbar;
    public TextMeshProUGUI Text;
    public GameObject CardObject;
    public event Action<CardView> Played;
    public bool InteractionsDisabled;
    
    private Vector3 _offset;
    private Vector3 _initialPosition;
    
    // Вообще это плохо. То есть должен быть чувак сверху,
    // который смотрит за вьюхами и за моделями и оркестрирует
    // ивентами. Но мне лень его писать, скоро спать надо. И
    // вообще, ООП отстой.
    //
    // Привет, это kawaii-Code из будущего, в 23:30 🤟🏻. Я добавил
    // чувака сверху и всё стало норм. Правда, ООП все ещё отстой.
    public Card CardThatWeCurrentlyDisplay;

    // card может быть null. А как же иначе?
    public void Show(Card card) {
        if (card == null) {
            CardObject.SetActive(false);
            return;
        }
        
        if (card.Config.Type.IsSpell()) {
            Healthbar.gameObject.SetActive(false);
        } else {
            Healthbar.gameObject.SetActive(true);
            Healthbar.SetColor(card.Config.Color);
        }
        CardObject.SetActive(true);
        Healthbar.Show(card.CurrentHealth);
        Text.color = card.Config.Color;
        Text.text = card.Config.Name;
        CardThatWeCurrentlyDisplay = card;
    }
    
    public void OnClick() {
        if (InteractionsDisabled) {
            return;
        }
        //Played?.Invoke(this);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        DescriptionOn?.Invoke(CardThatWeCurrentlyDisplay.Config.Description);
    }

    public void OnPointerExit(PointerEventData eventData) {
        DescriptionOff?.Invoke();
    }
    
    public void OnBeginDrag() {
        if (InteractionsDisabled) {
            return;
        }
        _offset = transform.position - Input.mousePosition;
        _initialPosition = transform.position;
    }
    
    public void OnDrag() {
        if (InteractionsDisabled) {
            return;
        }
        transform.position = _offset + Input.mousePosition;
    } 

    public void OnEndDrag() {
        if (InteractionsDisabled) {
            return;
        }
        
        if (transform.position.y > 420) {
            Played?.Invoke(this);   
        }
        transform.position = _initialPosition;
    }
}