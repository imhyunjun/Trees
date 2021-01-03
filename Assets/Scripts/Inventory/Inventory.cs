﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : PanelSingletone<Inventory>                     //인벤토리를 싱글톤 형태로 바꿈
{
    [SerializeField]
    private Transform inventoryItemsPool;                               //얻은 아이템들 여기에 임시로 두기

    [HideInInspector]
    public List<Slot> slotList = new List<Slot>(6);                     //슬롯관리
    private Slot clickedSlot;                                           //클릭된 슬롯
    private int maxSlotCount;                                           //최대 슬롯 개수

    public readonly static int slotCount = 6;

    private void Awake()
    {
        maxSlotCount = 6;
        SelectSlot(null);

        for (int i = 0; i < maxSlotCount; i++)
        {
            slotList.Add(gameObject.transform.GetChild(0).GetChild(i).GetComponent<Slot>());            //슬롯리스트에 슬롯 추가
        }
    }

    private void Update()
    {
        if (DialoguePanel.instance.IsDialogueOn()) return;           // 대화 중에는 클릭 안되게

        Vector3 inputPos;
        if (Application.platform == RuntimePlatform.WindowsEditor && Input.GetMouseButtonDown(0))        // Editor용 코드
            inputPos = Input.mousePosition;
        else if (Application.platform == RuntimePlatform.Android && Input.touchCount == 1)          // 안드로이드용 코드
            inputPos = Input.GetTouch(0).position;
        else return;

        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(inputPos), Vector2.zero, Mathf.Infinity); // 자꾸 콜라이더가 겹쳐서 레이를 쏴서                                                                                                                                                                                                                     //충돌하는 모든 오브젝트 받아오게 변경했습니다
        for (int i = 0; i < hits.Length; i++)
        {
            Debug.Log($"Inventory detect {hits[i].collider.gameObject.name}");
            if (hits[i].collider.transform.tag == "Item")
            {
                Item clickedItem = hits[i].collider.GetComponent<Item>();
                StartCoroutine(GetItemPanel.instance.IShowText(clickedItem.itemName));
                GetItemInSlot(hits[i].collider.gameObject);
                SoundManager.PlaySFX("get_item");
                return;
            }

            if (clickedSlot != null && clickedSlot.IsSlotHasItem)                 // 아이템 사용
            {
                Item clickedSlotItem = clickedSlot.hasItem;
                if (hits[i].collider.name == clickedSlotItem.canInteractWith)   //인벤토리에서 물건을 꺼내고 상호작용하는 물체와 이름이 같다면 아이템 사용
                {
                    if (clickedSlotItem.CanUse()) // 사용 가능하다면
                    {
                        clickedSlot.UseItem(clickedSlotItem.useType);       //아이템 사용 타입에 맞게 사용
                        clickedSlotItem.UseItem();
                        if (clickedSlotItem.useType != UseType.Repeat)
                            SelectSlot(null);
                    }
                    else
                    {
                        clickedSlotItem.FailToUse(); // 사용 실패
                        SelectSlot(null);
                    }
                    return;
                }
            }
        }
        SelectSlot(null);
    }

    public void SelectSlot(Slot slot)
    {
        if (clickedSlot != null) // 이전에 선택한 슬롯이 있다면 선택 해제
        {
            clickedSlot.DeSelect();
        }

        clickedSlot = slot;

        if (clickedSlot != null)   // 슬롯 선택
        {
            clickedSlot.Select();
            if (clickedSlot.IsSlotHasItem && clickedSlot.hasItem.useType == UseType.Immediately) // 선택한 슬롯의 아이템이 즉시 사용하는 아이템이면
            {
                if (clickedSlot.hasItem.CanUse()) // 사용 가능하면
                {
                    clickedSlot.hasItem.UseItem();
                    clickedSlot.UseItem(clickedSlot.hasItem.useType);
                }
                else
                    clickedSlot.hasItem.FailToUse();
                SelectSlot(null);
            }
        }
    }


    public void GetItemInSlot(GameObject item)
    {
        item.transform.SetParent(inventoryItemsPool); // 임시로 여기에 두고 안보이게 하기
        item.transform.localPosition = Vector3.zero;

        Item clickedItem = item.GetComponent<Item>();

        foreach (Slot slot in slotList)
        {
            if (!slot.IsSlotHasItem)                                        //슬롯이 비어있다면 아이템 정보 추가
            {                                                               //모든 슬롯이 꽉차이는 경우는 아직 x
                slot.GetItem(clickedItem);
                clickedItem.GetItem();
                break;
            }
        }
    }

    /// <summary>
    /// 인벤토리에 물건들이 있는지
    /// </summary>
    /// <param name="args"></param> 물건들
    /// <returns>
    /// true : 물건이 모두 있음
    /// false : 물건이 모두 없음
    /// </returns>
    public bool IsPlayerHasItem(params string[] args)
    {
        int count = 0;
        foreach (Slot slot in slotList)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (slot.hasItem != null && slot.hasItem.itemName == args[i])               //아이템이 있으면
                    count++;                                        //count ++;
                if (count == args.Length)                           //아이템이 다 있으면 true 반환
                    return true;
            }
        }
        return false;                                               //하나라도 없으면 false;
    }

    public bool IsPlayerHasItem(params System.Type[] args)
    {
        int count = 0;
        foreach (Slot slot in slotList)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (slot.hasItem != null && slot.hasItem.GetType() == args[i])
                    count++;
                if (count == args.Length)
                    return true;
            }
        }
        return false;
    }
    /// <summary>
    /// 인벤토리에 물건들이 없는지
    /// </summary>
    /// <param name="args"></param> 물건들
    /// <returns>
    /// true : 물건이 모두 없음
    /// false : 물건이 하나라도 있음
    /// </returns>
    public bool IsPlayerDoesntHaveItem(params string[] args)
    {
        int count = 0;
        foreach (Slot slot in slotList)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (slot.hasItem != null && slot.hasItem.itemName == args[i])               //아이템이 있으면
                    count++;                                        //count ++;
            }

        }
        if (count == 0)
            return true;
        else                    // 모두 없으면 true 하나라도 없으면 false
            return false;
    }

    /// <summary>
    /// 아이템 재사용(다시 인벤토리로)
    /// </summary>
    /// <param name="_canUseAgain"></param>true면 재사용, false면 아님
    public void ReUseItem(bool _canUseAgain, GameObject _gameObject)
    {
        if (_canUseAgain)
        {
            Item clickedItem = _gameObject.GetComponent<Item>();
            foreach (Slot slot in slotList)
            {
                if (!slot.IsSlotHasItem)                                        //슬롯이 비어있다면 아이템 정보 추가
                {                                                               //모든 슬롯이 꽉차이는 경우는 아직 x
                    slot.GetItem(clickedItem);
                    clickedItem.GetItem();
                    break;
                }
            }
        }
    }

    /// <summary>
    /// 인벤토리의 아이템에 상호작용하는 물체 변경
    /// </summary>
    /// <param name="_item"></param>    Card 등등
    /// <param name="_interactObject"></param>  앞으로 상호작용할 물체
    public void ChangeInteractObjectInInven(System.Type _item, string _interactObject)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.hasItem != null && slot.hasItem.GetType() == _item)
            {
                slot.hasItem.canInteractWith = _interactObject;
            }
        }
    }

    public void DeleteItemInSlot(Item item)
    {
        foreach (Slot slot in slotList)
        {
            if (slot.hasItem != null && slot.hasItem == item)
            {
                slot.hasItem.UseItem();
                slot.UseItem(UseType.Immediately);
            }
        }
    }

    public void ApplyInvenState(List<InvenData> invenData)
    {
        for(int i = 0; i < invenData.Count; i++)
        {
            InvenData data = invenData[i];
            if (data.hasItemType != null)
            {
                Item item = ObjectManager.GetObject(data.hasItemType).GetComponent<Item>();
                item.transform.SetParent(inventoryItemsPool); // 임시로 여기에 두고 안보이게 하기
                item.transform.localPosition = Vector3.zero;
                slotList[i].GetItem(item);
            }
        }
    }
}
