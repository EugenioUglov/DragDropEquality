using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IContentGetter
{
    public enum TypeEnum
    {
        ExpressionDigit,
        ResultDigit,
        Operator,
    }

    public TypeEnum Type;
    private GameObject _elementInSlot = null;


    public void OnDrop(PointerEventData eventData)
    {
        print("OnDrop");

        if (eventData.pointerDrag != null)
        {
            _elementInSlot = eventData.pointerDrag;

            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            eventData.pointerDrag.GetComponent<DragDrop>().IsInSlot = true;
        }
    }

    public GameObject GetItemFromSlot()
    {
        return _elementInSlot;
    }

    public string GetContent()
    {
        if (_elementInSlot == null) return null;

        return _elementInSlot.GetComponent<NumberItem>().GetContent();
    }
}
