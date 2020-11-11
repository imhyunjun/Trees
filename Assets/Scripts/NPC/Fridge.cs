﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Fridge : NPC
{
    public override void Interact()
    {
        Debug.Log("2");
        //쩅그랑 소리 추가 이곳 아니면 알코올의 GetItem함수에 추가
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashNCard)         //카드를 가진상태면
        {
            Debug.Log("1");
            Alcohol alcohol = GameManager.GetObject<Alcohol>();
            alcohol.gameObject.SetActive(true);
            Inventory.instance.GetItemInSlot(alcohol.gameObject);                             //술을 가져오고
            PlayerScan.instance.progressStatus = ProgressStatus.E_GetAlcholBottle;      //술을 가진 상태로 변경
        }
    }
}
