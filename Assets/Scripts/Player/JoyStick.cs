using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float range;

    private RectTransform leverTransform;
    private Vector2 firstPosition;
    

    private void Awake()
    {
        leverTransform = transform.GetChild(0).GetComponent<RectTransform>();
        firstPosition = leverTransform.anchoredPosition;
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        Debug.LogError(_eventData.position);
        leverTransform.anchoredPosition = _eventData.position - firstPosition;

    }

    public void OnDrag(PointerEventData _eventData)
    {
        Debug.LogError("드래그 중");
        leverTransform.anchoredPosition = RestrictRange(_eventData);
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        Debug.LogError("드래그 끝");
        leverTransform.anchoredPosition = firstPosition;
    }

    public Vector2 RestrictRange(PointerEventData _eventData)
    {
        if (_eventData.position.magnitude < range)
            return _eventData.position;
        else
            return (_eventData.position - firstPosition).normalized * range;
    }
}
