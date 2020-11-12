using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherRoomDoor : Door
{
    private void Awake()
    {
        isOpened = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if(status == ProgressStatus.E_JungWannaKillFather)
        {
            isOpened = true;
        }
    }
}
