﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class dctrDragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler{
    
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake() {
        rectTransform =  GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData){
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("OnEndDrag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnPointerDown(PointerEventData PointerEventData){
        Debug.Log("Test");
    }

}