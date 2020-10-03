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
            StartCoroutine(DialogueManager.instance.IContinueDialogue("prologue_10"));
            if (PlayerScan.instance.progressStatus == ProgressStatus.E_Sleep)
                PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithTreeFirstTime; // 나무와 첫 대화 후 과거 방 문 열림
        }
        else if(status == ProgressStatus.E_GetBackMirror)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue("prologue_15")); // 뭐 좀 가져 왔니?
        }
    }
}
