using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastMother : NPC
{
    public override void Interact()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_TalkWithTreeFirstTime)
        {
            DialogueManager.instance.PlayDialogue("prologue_11");
            PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithPastMom;
        }
    }
}
