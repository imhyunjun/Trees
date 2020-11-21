using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToHouseDoor : Door
{
    [SerializeField]
    private FrontDoor frontdoor;
    [SerializeField]
    private RoomDoor roomdoor;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if (status == ProgressStatus.E_PayedDone)        //물건을 다 사고 집에 들어오면 못나감
        {
            frontdoor.isOpened = false;                 //다 닫기
            roomdoor.isOpened = false;
            //RealWorldDoorManager.Instance.OpenCloseDoor(typeof(FrontDoor), false);
            //RealWorldDoorManager.Instance.OpenCloseDoor(typeof(RoomDoor), false);

        }
    }
}