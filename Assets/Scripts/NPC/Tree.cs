using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : NPC
{
    public override void Interact()
    {
        if (PlayerScan.instance.progressStatus < ProgressStatus.E_GetBackMirror)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(11, 22, DialogueManager.instance.currentProcedureIndexS, DialogueManager.instance.currentProcedureIndexE));
        }
        else if(PlayerScan.instance.progressStatus == ProgressStatus.E_GetBackMirror)
        {
            //
        }
    }
}
