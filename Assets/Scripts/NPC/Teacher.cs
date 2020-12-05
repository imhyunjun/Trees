using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : NPC
{
    int talkCount = 0;          //정이 선생님께 말 건 횟수
    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if(status == ProgressStatus.E_TeacherCallJung || status == ProgressStatus.E_JungGotShocked)
        {
            if (talkCount == 0)
            {
                //말풍선 띄우기
                talkCount++;
                PlayerScan.instance.progressStatus = ProgressStatus.E_JungGotShocked;
            }
            else if(talkCount < 5)
            {
                int randomBallon = Random.Range(0, 2);      //말풍선 두개 중 하나 정할 난수
                //말풍선 띄우기
                talkCount++;
            }
            else
            {
                //말풍선 띄우기
                talkCount = 0;                              //초기화
            }
        }
    }
}
