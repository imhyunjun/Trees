using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSuperMarketDoor : Door
{
    [SerializeField]
    private GameObject marketOwner;

    public override void AfterPlayerArrived()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashNCard)
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(marketOwner, "chapter_0_7"));
    }

}
