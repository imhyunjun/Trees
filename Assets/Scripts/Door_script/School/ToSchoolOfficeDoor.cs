using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSchoolOfficeDoor : Door
{

    public override void AfterPlayerArrived()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (status == ProgressStatus.E_TeacherCallJung)
        {
            //4명 말풍선 팝업
        }
    }
}
