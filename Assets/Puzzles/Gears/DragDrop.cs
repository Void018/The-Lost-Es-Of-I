using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    RectTransform rectTransform;
    GearControl gear;
    [SerializeField]
    private Vector2 dragSize;
    [SerializeField]
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private Vector2 size;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        gear = GetComponent<GearControl>();
        size = rectTransform.sizeDelta;
        dragSize = rectTransform.sizeDelta * 0.5f;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        gear.enabled = false;
        canvasGroup.blocksRaycasts = false;
        transform.GetComponent<CircleCollider2D>().enabled = false;
        rectTransform.sizeDelta = dragSize;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        gear.enabled = true;
        canvasGroup.blocksRaycasts = true;
        transform.GetComponent<CircleCollider2D>().enabled = true;
        rectTransform.sizeDelta = size;
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
