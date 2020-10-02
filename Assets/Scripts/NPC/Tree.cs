using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : NPC
{
    public override void Interact()
    {
        if (PlayerScan.Instance.countOfJungMetTree == 0)
        {
            StartCoroutine(DialogueManager.Instance.IContinueDialogue(11, 22, DialogueManager.Instance.currentProcedureIndexS, DialogueManager.Instance.currentProcedureIndexE));
        }
    }
}
