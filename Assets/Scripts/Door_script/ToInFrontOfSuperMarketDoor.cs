using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInFrontOfSuperMarketDoor : Door
{
    public override void AfterPlayerArrived()
    {
        GameManager.instance.player.transform.localScale *= 0.7f;
    }
}
