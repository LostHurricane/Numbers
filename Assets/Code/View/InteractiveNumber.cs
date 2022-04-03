using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class InteractiveNumber : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public TextMeshProUGUI Text;

    public int Number { get; private set; }

    public CanvasGroup CanvasGroup;

    public bool IsOnTheGrid = true;

    public RectTransform RectTransform;

    [SerializeField]
    private Canvas _canvas;

    public event Action<InteractiveNumber, InteractiveNumber> CompareNumbers;

    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    public void GetNumberFromText()
    {
        Number = int.Parse(Text.text);
    }

    public void SetNumber(int number)
    {
        Number = number;
        Text.text = number.ToString();
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!IsOnTheGrid)
        {
            CanvasGroup.alpha = 0.6f;
            CanvasGroup.blocksRaycasts = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        if (!IsOnTheGrid)
        {
            RectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CanvasGroup.alpha = 1f;
        CanvasGroup.blocksRaycasts = true;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (IsOnTheGrid)
        {
            if (eventData.pointerDrag.TryGetComponent<InteractiveNumber>(out var interactiveNumber))
            {
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition * 2;
                if (CompareNumbers != null)
                {
                    CompareNumbers.Invoke(this, interactiveNumber);
                }
            }
        }
    }
}
