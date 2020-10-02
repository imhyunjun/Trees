using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private List<Slot> slotList = new List<Slot>();                  //슬롯관리
    private Slot clickedSlot;                                                //클릭된 슬롯
    private int maxSlotCount;                                   //최대 슬롯 개수

    private void Awake()
    {
        maxSlotCount = 6;
        clickedSlot = null;

        for(int i = 0; i < maxSlotCount; i ++)
        {
            slotList.Add(gameObject.transform.GetChild(i).GetComponent<Slot>());            //슬롯리스트에 슬롯 추가
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
                    case "Item":

                        Item clickedItem = hit.collider.GetComponent<Item>();

                        StartCoroutine(GetItemPanel.instance.IShowText(clickedItem.itemName)); 

                        //DialogueManager.instance.currentProcedureIndexS = clickedItem.increaseDialogueStart;
                        //DialogueManager.instance.currentProcedureIndexE = clickedItem.increaseDialogueEnd;

                        hit.collider.gameObject.SetActive(false);                                   //클릭한 오브젝트 비활성화

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
                        break;

                    case "Slot":
                        clickedSlot = hit.collider.GetComponent<Slot>();
                        if(clickedSlot.isSlotHasItem)          //슬롯에 아이템이 있으면, 클릭된 아이템이 없으면
                        {
                            clickedSlot.tempColor.a = 0.5f;                             //아이템 투명도 반
                            clickedSlot.hasItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            //아이템이눈에 보이는지 아닌지는 나중에 물체마다 설정
                        }
                        break;
                }
                if (clickedSlot != null && clickedSlot.isSlotHasItem)
                {
                    Item clickedSlotItem = clickedSlot.hasItem; 
                    if (hit.collider.name == clickedSlotItem.canInteractWith)   //인벤토리에서 물건을 꺼내고 상호작용하는 물체와 이름이 같다면
                    {
                        clickedSlotItem.UseItem();                       // 해당 아이템을 사용했을 때 발생하는 이벤트
                        clickedSlotItem.gameObject.SetActive(false);                      
                        clickedSlot.hasItem = null;
                        clickedSlot.hasItemSprite = clickedSlot.slotDefaultSprite;

                        //활성화된 물체는 비활성 코드 나중 추가
                        clickedSlot.tempColor.a = 1f;
                        clickedSlot = null;
                    }
                }
            }
            else if(clickedSlot != null && clickedSlot.hasItem != null)
            {
                clickedSlot.hasItem.transform.position = Vector3.zero;
                clickedSlot.tempColor.a = 1f;
                clickedSlot = null;
            }
        }
    }
}
