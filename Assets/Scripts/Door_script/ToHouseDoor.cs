using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHouseDoor : Door
{
    public override void OnUseDoor()
    {
        PlayerMove.canMove = false;
    }
    public override void AfterPlayerArrived()
    {
        GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = false;
        Invoke("CanMove", 0.5f); // 강제로 못움직이게하고 0.5초뒤에 움직일 수 있게 해줌
    }

    private void CanMove()
    {
        PlayerMove.canMove = true;
    }
}