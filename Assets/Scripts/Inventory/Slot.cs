using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Image image;                   // 아이템 이미지 컴퍼넌트
    private Sprite slotDefaultSprite;        //슬롯 기본 이미지

    public bool isSlotHasItem => hasItem != null;  //슬롯에 아이템이 있는지
    public Item hasItem;              //가지고 있는 아이템

    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        slotDefaultSprite = image.sprite;
        hasItem = null;
    }

    public void OnClick()   // 슬롯 버튼이 눌렸을 때
    {
        Inventory.instance.SelectSlot(this);
    }

    public void Select()      // 슬롯이 선택 되었을 때
    {
        if (isSlotHasItem)
        {
            Color color = image.color;
            color.a = 0.5f;
            image.color = color;
        }
    }

    public void DeSelect()      // 슬롯 선택이 해지되었을 때
    {
        Color color = image.color;
        color.a = 1f;
        image.color = color;
    }

    public void GetItem(Item item)      // 슬롯에 아이템이 들어왔을 때
    {
        hasItem = item;
        image.sprite = item.itemSprite;
    }

    public void UseItem()           // 슬롯에 있는 아이템을 사용했을 때
    {
        hasItem = null;
        image.sprite = slotDefaultSprite;
    }

}
