using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromAccidentToTreeRoom : Door
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if (status == ProgressStatus.E_JungWannaKillFather)
            Inventory.instance.ChangeInteractObjectInInven(typeof(BrokenBottle), "Tree");
    }
}
