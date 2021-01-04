using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] private float range;
    private RectTransform leverTransform;
    private RectTransform rectTransform;

    private void Awake()
    {
        leverTransform = transform.GetChild(0).GetComponent<RectTransform>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData _eventData)
    {
        leverTransform.anchoredPosition = _eventData.position - rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        leverTransform.anchoredPosition = RestrictRange(_eventData);
        PlayerMove.isDraging = true;
    }

    public void OnEndDrag(PointerEventData _eventData)
    {
        leverTransform.anchoredPosition = Vector2.zero;
        PlayerMove.isDraging = false;
    }

    public Vector2 RestrictRange(PointerEventData _eventData)
    {
        Vector2 inputVec = _eventData.position - rectTransform.anchoredPosition;
        PlayerMove.playerMoveVec = inputVec.normalized;
        if (inputVec.magnitude <= range)
            return inputVec;
        else
            return inputVec.normalized * range;
    }
}
