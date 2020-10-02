using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : NPC
{
    [SerializeField] Sprite sleepingJung;     //일단 임시 방편 for 시연회

    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if(status == ProgressStatus.E_Start)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(7, 7));
        }
        else if(status == ProgressStatus.E_ChangeClothes)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(8, 8));
        }
        else if(status == ProgressStatus.E_EatMedicine)
        {
            PlayerScan.instance.transform.Find("bed").gameObject.GetComponent<AudioSource>().pitch = 3;
            GetComponent<SpriteRenderer>().sprite = sleepingJung;
            PlayerScan.instance.GetComponent<SpriteRenderer>().sortingOrder = -1;
            StartCoroutine(GameManager.instance.IFadeOut(5f));
            StartCoroutine(GameManager.instance.ILoadScene("DreamMap", 5f, "DreamMap"));
            GameManager.instance.gameSceneProcedure++;
            PlayerScan.instance.progressStatus++;
        }
    }
}
