using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : NPC
{
    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if (status < ProgressStatus.E_GetBackMirror)
        {
            DialogueManager.instance.PlayDialogue("prologue_10", false, () =>
            {
                if (PlayerScan.instance.progressStatus == ProgressStatus.E_Sleep)
                {
                    PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithTreeFirstTime; // 나무와 첫 대화 후 과거 방 문 열림
                    SoundManager.PlaySFX("door-open"); // 문열리는 효과음
                }
            });
        }
        else if(status == ProgressStatus.E_GetBackMirror)
        {
            DialogueManager.instance.PlayDialogue("prologue_15"); // 뭐 좀 가져 왔니?
        }
        else if(status == ProgressStatus.E_ErrandFinished)
        {
            DialogueManager.instance.PlayDialogue("chapter_0_19", false, ()=> {  // 어디 다녀왔어~~?
                PlayerScan.instance.progressStatus = ProgressStatus.E_JungWannaKillFather;
                ObjectManager.GetObject<FatherRoomDoor>().isOpened = true;
                SoundManager.PlaySFX("door-open"); // 문열리는 효과음
            });
        }
        else if(status == ProgressStatus.E_EndMirrorRoom)
        {
            DialogueManager.instance.PlayDialogue("chapter_2_10", false, ()=> { PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithTreeAfterMirror; }); // 얼굴에 나비 멋지다
        }
    }
}
