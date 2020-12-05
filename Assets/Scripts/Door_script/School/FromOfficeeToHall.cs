using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromOfficeeToHall : Door
{

    public override void AfterPlayerArrived()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if(status == ProgressStatus.E_JungGotShocked)
        {
            GameManager.instance.MoveJungCor(5f, 4f, "LivingRoom");
            GameManager.instance.player.transform.position = ObjectManager.GetObject<Front>().transform.position;
            
        }
    }
}
