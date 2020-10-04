using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : Item
{
    [SerializeField]
    private Front front;
    [SerializeField]
    private FrontDoor frontDoor;

    public override void GetItem()
    {
        base.GetItem();
        if (Inventory.instance.IsPlayerHasItem(typeof(Card), typeof(Cash)))   // 천원과 카드를 모두 가졌으면
        {
            front.CanPass(true);
            frontDoor.isOpened = true;                              // 현관문 열림
            PlayerScan.instance.progressStatus = ProgressStatus.E_GetCashNCard;
        }
    }
}
