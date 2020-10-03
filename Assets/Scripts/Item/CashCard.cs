using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCard : Item
{
    [SerializeField]
    private Front front;
    [SerializeField]
    private FrontDoor frontDoor;

    public override void GetItem()
    {
        base.GetItem();
        front.CanPass(true);
        frontDoor.isOpened = true;                              // 현관문 열림
        PlayerScan.instance.progressStatus = ProgressStatus.E_GetCashCard;
    }
}