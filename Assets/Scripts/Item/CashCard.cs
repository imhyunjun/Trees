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
        PlayerScan.instance.progressStatus = ProgressStatus.E_GetCashNCard;
        Inventory.instance.DeleteItemInSlot(this); // 캐쉬카드는 없어지고 천원, 카드 각각으로 들어감
        Inventory.instance.GetItemInSlot(card.gameObject);
        Inventory.instance.GetItemInSlot(cash.gameObject);
        card.gameObject.SetActive(true);
        cash.gameObject.SetActive(true);
    }
}
