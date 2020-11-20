using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FatherRoomDoor : Door
{
    [SerializeField]
    private BrokenBottle brokenBottle;
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
            Inventory.instance.ChangeInteractObjectInInven(typeof(BrokenBottle), "FatherMonster");
        }
    }
}
