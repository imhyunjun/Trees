using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInFrontOfHouseDoor : Door
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOpened = true;
    }

    public override void AfterPlayerArrived()
    {
        GameManager.instance.player.transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
