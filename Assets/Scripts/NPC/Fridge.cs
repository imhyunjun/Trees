using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : NPC
{
    GameObject alcholBottle;                                //임시용

    public override void Interact()
    {
        //쩅그랑 소리 추가
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashCard)         //카드를 가진상태면
        {
            Inventory.instance.GetItemInSlot(alcholBottle);                             //술을 가져오고
            PlayerScan.instance.progressStatus = ProgressStatus.E_GetAlcholBottle;      //술을 가진 상태로 변경
        }
    }
}
