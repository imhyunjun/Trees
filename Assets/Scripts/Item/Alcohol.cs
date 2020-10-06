﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcohol : Item
{
    [SerializeField]
    private GameObject counter;
    [SerializeField]
    private GameObject marketOwner;
    [SerializeField]
    private GameObject father;

    public override void UseItem()
    {
        base.UseItem();

        if (PlayerScan.instance.progressStatus == ProgressStatus.E_PayedDone)
        {
            //놓는 사운드 추가
            gameObject.SetActive(true);
            Vector3 tempvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempvec.z = 0;
            gameObject.transform.position = tempvec;
            if (Inventory.instance.IsPlayerDoesntHaveItem("카드"))
            {
                DialogueManager.instance.IShowDialogueBalloon(father, "chapter_7");              // 네 방으로 들어가
                PlayerScan.instance.progressStatus = ProgressStatus.E_ErrandFinished;
            }
        }
        else if(PlayerScan.instance.progressStatus == ProgressStatus.E_GetAlcholBottle)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetParent(counter.transform);
            gameObject.transform.position = counter.transform.position;                                         //카운터 중간(나중에 확인)
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(marketOwner, "chapter_4"));            //5천원 카드니?

        }
    }
}
