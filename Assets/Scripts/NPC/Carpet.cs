using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerScan.instance.progressStatus == ProgressStatus.E_PayedDone)            //계산이 다 끝난 후라면
        {
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(GameManager.GetObject<Father>().gameObject, "chapter_6"));               //나중에 null 대신 아빠
            
            Inventory.instance.ChangeInteractObjectInInven("술", "Table");                       //카드와 술(검정봉투)의 상호작용 변경
            Inventory.instance.ChangeInteractObjectInInven("카드", "Table");
        }
    }
}
