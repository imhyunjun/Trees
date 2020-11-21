using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCard : Item
{
    [SerializeField]
    private Front front;
    [SerializeField]
    private FrontDoor frontDoor;
    [SerializeField]
    private Card card;
    [SerializeField]
    private Cash cash;

    public override void GetItem()
    {
        base.GetItem();
        front.CanPass(true);
        frontDoor.isOpened = true;                              // 현관문 열림
        //RealWorldDoorManager.Instance.OpenCloseDoor(typeof(FrontDoor), true);
        PlayerScan.instance.progressStatus = ProgressStatus.E_GetCashNCard;
        Inventory.instance.DeleteItemInSlot(this); // 캐쉬카드는 없어지고 천원, 카드 각각으로 들어감
        card.gameObject.SetActive(true);
        cash.gameObject.SetActive(true);
        Inventory.instance.GetItemInSlot(card.gameObject);
        Inventory.instance.GetItemInSlot(cash.gameObject);
        
    }
}
