using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighter : Item
{
    [SerializeField]
    private float useMaxDistance = 8f;
    [SerializeField]
    private TeacherMonster teacherMon;

    public override void GetItem()
    {
        base.GetItem();
        teacherMon.Move();
    }

    public override bool CanUse()
    {
        if (canInteractWith == "TeacherMonster")
            return teacherMon.isMoving && Vector3.Distance(GameManager.instance.player.transform.position, teacherMon.transform.position) < useMaxDistance; // 선생님 몬스터와의 거리가 멀면 사용 불가
        else if (canInteractWith == "Tree")
            return PlayerScan.instance.progressStatus == ProgressStatus.E_TalkWithTreeAfterMirror;
        return base.CanUse();
    }

    public override void FailToUse()
    {
        if(canInteractWith == "TeacherMonster")
            DialogueManager.instance.PlayDialogue("chapter_2_9"); // 당신도 그 얼굴에 끔찍한 화상을 입게 할꺼야
    }

    public override void UseItem()
    {
        base.UseItem();
        if (canInteractWith == "TeacherMonster")
            teacherMon.BurnAndMoveToStudents();                 // 선생님 몬스터 얼굴 불타오르고 학생들을 향해서 이동
        else if(canInteractWith  == "Tree")
            GameManager.instance.treeGrowStatus++;              // 나무 성장
    }
}
