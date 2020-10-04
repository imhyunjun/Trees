using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSuperMarketDoor : Door
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashNCard)
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(null, "chapter_3"));
    }
}
