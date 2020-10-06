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

        if (!Inventory.instance.IsPlayerDoesntHaveItem("술", "카드") && status == ProgressStatus.E_PayedDone)
        //오브젝트 이름은 바뀌면 바꿔주세요 일단 Card, 인벤토리 알코올 or 카드가 있으면
        {
            isOpened = false;
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(null, "chapter_8"));
        }
        else if(status == ProgressStatus.E_ErrandFinished)
        {
            isOpened = false;
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(null, "chapter_11"));
        }
    }
}
