using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : NPC
{
    [SerializeField]
    private Sprite reflected;
    [SerializeField]
    private TeacherMonster teacherMon;

    public override void Interact()
    {
        if(PlayerScan.instance.progressStatus == ProgressStatus.E_EatMedicine2)
        {
            GetComponent<SpriteRenderer>().sprite = reflected;          // 거울에 정이 비침
            // 정의 얼굴 흉터가 나비로 바뀜
            PlayerScan.instance.progressStatus = ProgressStatus.E_FaceScarToButterfly;
            teacherMon.Move();
        }
    }
}