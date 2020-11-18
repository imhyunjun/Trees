using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHouseDoor : Door
{
    public override void AfterPlayerArrived()
    {
        GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = false;
    }
}
