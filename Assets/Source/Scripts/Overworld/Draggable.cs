using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private Transform _grid;
    [SerializeField] private Transform _canvas;

    private void Start()
    {
        _grid = GetComponentInParent<GridLayoutGroup>().transform;
        _canvas = GetComponentInParent<Canvas>().transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
        transform.SetParent(_canvas);
    } 
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    } 

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        transform.SetParent(_grid);
    }
}
