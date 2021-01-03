﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerScan.instance.progressStatus == ProgressStatus.E_PayedDone)            //계산이 다 끝난 후라면
        {
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            list.Add(new KeyValuePair<GameObject, string>(ObjectManager.GetObject<Father>().gameObject, "chapter_6"));   // 테이블위에 올려놔
            DialogueManager.instance.ShowDialogueBallon(list);

            Inventory.instance.ChangeInteractObjectInInven(typeof(Alcohol), "Table");                       //카드와 술(검정봉투)의 상호작용 변경
            Inventory.instance.ChangeInteractObjectInInven(typeof(Card), "Table");
        }
    }
}
