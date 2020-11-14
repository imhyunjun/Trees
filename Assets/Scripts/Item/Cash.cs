using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cash : Item
{
    public override void GetItem()
    {
        base.GetItem();
        if (Inventory.instance.IsPlayerHasItem(typeof(Card), typeof(Cash)))   // 천원과 카드를 모두 가졌으면
        {
            GameManager.GetObject<Front>().CanPass(true);
            GameManager.GetObject<FrontDoor>().isOpened = true;                              // 현관문 열림
            PlayerScan.instance.progressStatus = ProgressStatus.E_GetCashNCard;
        }
    }
}
