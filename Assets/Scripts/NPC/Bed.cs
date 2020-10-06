using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : NPC
{
    [SerializeField]
    private Sprite sleepingJung;     //일단 임시 방편 for 시연회

    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if(status == ProgressStatus.E_Start)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue("prologue_5"));
        }
        else if(status == ProgressStatus.E_ChangeClothes)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue("prologue_6"));
        }
        else if(status == ProgressStatus.E_EatMedicine)
        {
            GoToDreamMap();
            PlayerScan.instance.progressStatus = ProgressStatus.E_Sleep;
        }
        else if(status == ProgressStatus.E_ErrandFinished)
        {
            GoToDreamMap();
        }
    }

    private void GoToDreamMap()
    {
        PlayerMove.canMove = false;
        SoundManager.PlaySFX("lying-on-bed");
        GetComponent<SpriteRenderer>().sprite = sleepingJung;
        PlayerScan.instance.GetComponent<SpriteRenderer>().sortingOrder = -1;
        GameManager.instance.StartLoadSceneCor("DreamMap", 5f, 2f, "DreamMap", () => { PlayerMove.canMove = true; });
    }
}
