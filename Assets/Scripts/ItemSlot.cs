using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IContentGetter
{
    // TO DO
    public enum TypeEnum
    {
        ExpressionDigit,
        ResultDigit,
        Operator,
    }

    public TypeEnum Type;
    //

    private GameObject _lastDroppedElementToSlot = null;


    public void OnDrop(PointerEventData eventData)
    {
        print("OnDrop");

        if (eventData.pointerDrag != null)
        {
            _lastDroppedElementToSlot = eventData.pointerDrag;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            eventData.pointerDrag.GetComponent<NumberItem>().IsInSlot = true;
            eventData.pointerDrag.GetComponent<NumberItem>().Slot = this;
        }
    }

    public GameObject GetItemFromSlot()
    {
        return _lastDroppedElementToSlot;
    }

    public void OnSlotCleared()
    {
        _lastDroppedElementToSlot = null;
    }

    public string GetContent()
    {
        if (_lastDroppedElementToSlot == null || _lastDroppedElementToSlot.GetComponent<NumberItem>().IsInSlot == false) return null;

        return _lastDroppedElementToSlot.GetComponent<NumberItem>().GetContent();
    }
}
