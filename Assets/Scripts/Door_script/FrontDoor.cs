using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : Door
{
    private void Start()
    {
        isOpened = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if(status == ProgressStatus.E_PayedDone)        //물건을 다 사고 집에 들어오면 못나감
        {
            isOpened = false;
        }
        else if (!Inventory.instance.IsPlayerDoesntHaveItem("술", "카드") && status == ProgressStatus.E_PayedDone)
        //오브젝트 이름은 바뀌면 바꿔주세요 일단 Card, 인벤토리 알코올 or 카드가 있으면
        {
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(GameManager.GetObject<Father>().gameObject, "chapter_8"));   //어딜 가는거야?
        }
        else if(status == ProgressStatus.E_ErrandFinished)
        {
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(GameManager.instance.player, "chapter_11"));  //방으로 들어가자
        }
    }
}
