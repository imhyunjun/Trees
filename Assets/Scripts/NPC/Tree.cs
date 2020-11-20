using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : NPC
{
    [SerializeField]
    private FatherRoomDoor _fatherRoomDoor;

    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if (status < ProgressStatus.E_GetBackMirror)
        {
            DialogueManager.instance.PlayDialogue("prologue_10");
            if (PlayerScan.instance.progressStatus == ProgressStatus.E_Sleep)
                PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithTreeFirstTime; // 나무와 첫 대화 후 과거 방 문 열림
        }
        else if(status == ProgressStatus.E_GetBackMirror)
        {
            DialogueManager.instance.PlayDialogue("prologue_15"); // 뭐 좀 가져 왔니?
        }
        else if(status == ProgressStatus.E_ErrandFinished)
        {
            DialogueManager.instance.PlayDialogue("chapter_0_19"); // 어디 다녀왔어~~?
            PlayerScan.instance.progressStatus = ProgressStatus.E_JungWannaKillFather;
            _fatherRoomDoor.isOpened = true;
            //문 여는 소리

        }
    }
}
