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

        if (!Inventory.instance.IsPlayerDoesntHasItem("Alcohol", "Card") && status == ProgressStatus.E_PayedDone)
        //오브젝트 이름은 바뀌면 바꿔주세요 일단 Card, 인벤토리 알코올 or 카드가 있으면
        {
            isOpened = false;
            DialogueManager.instance.IShowDialogueBalloon(null, "chapter_8");
        }
    }
}
