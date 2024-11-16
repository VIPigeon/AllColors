using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableCard : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected Transform _grid;
    [SerializeField] protected Transform _canvas;
    [SerializeField] protected CardConfig _card;
    [SerializeField] protected float _overlapRadius;

    private void Start()
    {
        _grid = GetComponentInParent<GridLayoutGroup>().transform;
        _canvas = GetComponentInParent<Canvas>().transform;
    }

    public void SetCard(CardConfig card)
    {
        _card = card;
        GetComponent<Image>().color = card.Color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(_canvas);
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    } 

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        Paintable closestPaintable = null;
        float minPaintableDistance = _overlapRadius + 1f;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(transform.position);
        foreach(Collider2D collider in Physics2D.OverlapCircleAll(worldPosition, _overlapRadius))
        {
            if(collider.TryGetComponent(out Paintable paintable))
            {
                if(Vector3.Distance(worldPosition, collider.transform.position) < minPaintableDistance)
                {
                    closestPaintable = paintable;
                    minPaintableDistance = Vector3.Distance(worldPosition, collider.transform.position);
                }
            }
        }

        if(closestPaintable != null)
        {
            closestPaintable.ApplyCard(_card);
            FullDeck.Instance.Cards.Remove(_card);
            Destroy(gameObject);
        }
        else
            transform.SetParent(_grid);
    }
}
