using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
    
public class Inventory : PanelSingletone<Inventory>                     //인벤토리를 싱글톤 형태로 바꿈
{
    [SerializeField]
    private Transform inventoryWorldItems;                              // 획득한 아이템들의 World 오브젝트를 임시로 보관할 곳

    private List<Slot> slotList = new List<Slot>();                     //슬롯관리
    private Slot clickedSlot;                                           //클릭된 슬롯
    private int maxSlotCount;                                           //최대 슬롯 개수

    private void Awake()
    {
        maxSlotCount = 6;
        clickedSlot = null;

        for(int i = 0; i < maxSlotCount; i ++)
        {
            slotList.Add(gameObject.transform.GetChild(0).GetChild(i).GetComponent<Slot>());            //슬롯리스트에 슬롯 추가
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (hit.collider != null)          //땅에 있는 아이템 클릭
            {
                string colliderTag = hit.collider.transform.tag;
                switch (colliderTag)
                {
                    case "Item":                                           // 아이템 획득

                        Item clickedItem = hit.collider.GetComponent<Item>();

                        if (clickedItem.isInInventory) break;                   // 인벤토리에 있는 아이템이라면 break

                        StartCoroutine(GetItemPanel.instance.IShowText(clickedItem.itemName)); 

                        //DialogueManager.instance.currentProcedureIndexS = clickedItem.increaseDialogueStart;
                        //DialogueManager.instance.currentProcedureIndexE = clickedItem.increaseDialogueEnd;

                        //hit.collider.gameObject.SetActive(false);                                   //클릭한 오브젝트 비활성화

                        hit.transform.SetParent(inventoryWorldItems);                  // 클릭한 오브젝트 부모 변경 후 위치 변경해서 안보이게
                        hit.transform.localPosition = Vector3.zero;

                        foreach (Slot slot in slotList)
                        {
                            if (!slot.isSlotHasItem)                                                 //슬롯이 비어있다면 아이템 정보 추가
                            {                                                                       //모든 슬롯이 꽉차이는 경우는 아직 x  나중에 추가
                                slot.hasItem = clickedItem;
                                slot.hasItemSprite = clickedItem.itemSprite;
                                clickedItem.GetItem();                                      // 해당 아이템을 얻었을 때 발생하는 이벤트
                                break;
                            }
                        }
                        break;

                    case "Slot":                                                         // 슬롯 선택
                        Slot click = hit.collider.GetComponent<Slot>();
                        SelectSlot(click);
                        break;
                }

                if (clickedSlot != null && clickedSlot.isSlotHasItem) 
                {
                    Item clickedSlotItem = clickedSlot.hasItem; 
                    if (hit.collider.name == clickedSlotItem.canInteractWith)   //인벤토리에서 물건을 꺼내고 상호작용하는 물체와 이름이 같다면 아이템 사용
                    {
                        clickedSlotItem.UseItem();                       // 해당 아이템을 사용했을 때 발생하는 이벤트
                        clickedSlot.hasItem = null;
                        clickedSlot.hasItemSprite = clickedSlot.slotDefaultSprite;
                        DeSelectSlot();
                    }
                }
            }
            else
            {
                DeSelectSlot();                           // 빈 공간 클릭했을 때 슬롯 선택 헤제
            }
        }
    }

    private void SelectSlot(Slot slot)
    {
        DeSelectSlot();                  // 이전에 선택한 슬롯이 있다면 선택 해제

        clickedSlot = slot;           // 슬롯 선택

        if (clickedSlot.isSlotHasItem)          //슬롯에 아이템이 있으면
        {
            clickedSlot.tempColor.a = 0.5f;                             //아이템 투명도 반
            clickedSlot.hasItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }

    private void DeSelectSlot()
    {
        if (clickedSlot == null) return;          // 선택된 슬롯이 없다면 리턴

        if (clickedSlot.isSlotHasItem) 
        {
            clickedSlot.hasItem.transform.position = Vector3.zero;
        }
        clickedSlot.tempColor.a = 1f;
        clickedSlot = null;
    }

    public void GetItemInSlot(GameObject item)
    {
        item.gameObject.SetActive(false);                                   //클릭한 오브젝트 비활성화
        Item clickedItem = item.GetComponent<Item>();
        foreach (Slot slot in slotList)
        {
            if (!slot.isSlotHasItem)                                                 //슬롯이 비어있다면 아이템 정보 추가
            {                                                                       //모든 슬롯이 꽉차이는 경우는 아직 x
                slot.hasItem = clickedItem;
                slot.hasItemSprite = clickedItem.itemSprite;
                clickedItem.GetItem();                                      // 해당 아이템을 얻었을 때 발생하는 이벤트
                break;
            }
        }
    }
}
