using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolDoor : Door
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if(status == ProgressStatus.E_TeacherCallJung)
        {
            DialogueManager.instance.PlayDialogue("chapter_2_6");       //교무실로 가자. 교무실은 오른쪽에 있어.
        }
    }
}
