using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastJung : NPC
{
    public override void Interact()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_TalkWithPastMom)
        {
            DialogueManager.instance.PlayDialogue("prologue_13");
            PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithPastJung;
        }
    }
}
