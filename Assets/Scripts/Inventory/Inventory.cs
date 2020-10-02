using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : PanelSingletone<Inventory>                 //인벤토리를 싱글톤 형태로 바꿈
{
    List<Slot> slotList = new List<Slot>();                         //슬롯관리
    //List<Item> itemList = new List<Item>();                         //아이템 정보 관리
    //List<GameObject> itemGameObjectList = new List<GameObject>();   //아이템 게임오브젝트 관리 (활성, 비활성)
    Slot clickedSlot;                                                //클릭된 슬롯

    int maxSlotCount;                                               //최대 슬롯 개수
    bool isItemClickedInInven;                                      //인벤토리안의 아이템이 클릭되었는지

    private void Awake()
    {
        maxSlotCount = 6;
        isItemClickedInInven = false;
        clickedSlot = null;

        for(int i = 0; i < maxSlotCount; i ++)
        {
            slotList.Add(gameObject.transform.GetChild(0).GetChild(i).GetComponent<Slot>());            //슬롯리스트에 슬롯 추가
            //itemList.Add(null);
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
                        GetItemInSlot(hit.collider.gameObject);
                        break;

                    case "Slot":
                        clickedSlot = hit.collider.GetComponent<Slot>();
                        if(clickedSlot.isSlotHasItem && !isItemClickedInInven)          //슬롯에 아이템이 있으면, 클릭된 아이템이 없으면
                        {
                            clickedSlot.tempColor.a = 0.5f;                             //아이템 투명도 반
                            isItemClickedInInven = true;
                            clickedSlot.hasItem.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                            //아이템이눈에 보이는지 아닌지는 나중에 물체마다 설정
                        }
                        break;
                }
                if (clickedSlot!= null && clickedSlot.isSlotHasItem)
                {
                    if (hit.collider.name == clickedSlot.hasItem.GetComponent<Item>().canInteractWith && isItemClickedInInven)   //인벤토리에서 물건을 꺼내고 상호작용하는 물체와 이름이 같다면
                    {
                        
                        DialogueManager.instance.currentProcedureIndexS += clickedSlot.hasItem.GetComponent<Item>().afterGiveTreeStart;
                        DialogueManager.instance.currentProcedureIndexE += clickedSlot.hasItem.GetComponent<Item>().afterGiveTreeEnd;

                        GameManager.instance.treeGrowStatus += clickedSlot.hasItem.GetComponent<Item>().changeTree; 
                        if(clickedSlot.hasItem.GetComponent<Item>().changeTree == 1)                            //나무 상태를 변화시키면
                        {
                            StartCoroutine(GameManager.instance.ILoadScene(_sceneName: "Prologue", _fadetime: 4, _playerIn: "House"));      //일반화 나중에
                        }
                        clickedSlot.hasItem.gameObject.SetActive(false);                      
                        clickedSlot.isSlotHasItem = false;
                        clickedSlot.hasItem = null;
                        clickedSlot.hasItemSprite = clickedSlot.slotDefaultSprite;

                        //활성화된 물체는 비활성 코드 나중 추가
                        isItemClickedInInven = false;
                        clickedSlot.tempColor.a = 1f;
                    }
                }
            }
            else if(clickedSlot != null && clickedSlot.hasItem != null)
            {
                clickedSlot.hasItem.transform.position = Vector3.zero;
                isItemClickedInInven = false;
                clickedSlot.tempColor.a = 1f;
            }
        }
    }

    public void GetItemInSlot(GameObject _item)
    {
        Item clickedItem = _item.GetComponent<Item>();

        StartCoroutine(GetItemPanel.instance.IShowText(clickedItem.itemName));

        DialogueManager.instance.currentProcedureIndexS = clickedItem.increaseDialogueStart;
        DialogueManager.instance.currentProcedureIndexE = clickedItem.increaseDialogueEnd;

        //itemList.Add(clickedItem);                                                //나중에 쓸진 모르겠으나 일단 리스트에 클릭한 아이템 저장
        _item.SetActive(false);                                   //클릭한 오브젝트 비활성화
                                                                                    //clickedItem.isInInventory = true;                                         //클릭한 아이템은 이제 인벤토리에

        foreach (Slot slot in slotList)
        {
            if (!slot.isSlotHasItem)                                                 //슬롯이 비어있다면 아이템 정보 추가
            {                                                                       //모든 슬롯이 꽉차이는 경우는 아직 x
                slot.isSlotHasItem = true;
                slot.hasItem = _item;
                slot.hasItemSprite = clickedItem.itemSprite;
                break;
            }
        }
    }
}
