using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private HandManager handManager;

    public void Initialize(HandManager manager)
    {
        handManager = manager;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        handManager.OnCardBeginDrag(gameObject, eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        handManager.OnCardDrag(gameObject, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handManager.OnCardEndDrag(gameObject, eventData);
    }
}