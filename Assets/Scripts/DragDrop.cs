using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public event Action OnItemDroppedToSlot;

    [SerializeField] private Canvas _canvas;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private Vector3 _beginPosition;
    private Vector3 _startAnchoredPosition;
    private Color _defaultColor;
    private NumberItem _numberItem;


    private void Awake() 
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _numberItem = GetComponent<NumberItem>();
    }

    private void Start() 
    {
        _beginPosition = transform.position;
        _defaultColor = gameObject.GetComponent<Image>().color;
    }


    public void SetCanvas(Canvas canvas)
    { 
        _canvas = canvas;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GetComponent<NumberItem>().Slot != null)
        {
            GetComponent<NumberItem>().Slot.OnSlotCleared();
            GetComponent<NumberItem>().Slot = null;
        }

        
        gameObject.GetComponent<Image>().color = _defaultColor;

        if (GetComponent<NumberItem>().IsInSlot == false)
        {
            _startAnchoredPosition = _rectTransform.anchoredPosition;
        }
        
        print(_startAnchoredPosition);
        print("OnBeginDrag");
        GetComponent<NumberItem>().IsInSlot = false;
        _canvasGroup.alpha = .6f;
        _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("OnEndDrag");
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;

        if (GetComponent<NumberItem>().IsInSlot)
        {
            OnItemDroppedToSlot?.Invoke();
            gameObject.GetComponent<Image>().color = new Color(130, 241, 144);
            return;
        }

        _rectTransform.anchoredPosition = _startAnchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("OnPointerDown");
        // _beginPosition = transform.position;
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("OnDrop");
    }
}
