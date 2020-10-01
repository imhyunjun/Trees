using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : NPC
{
    [SerializeField] Sprite sleepingJung;     //일단 임시 방편 for 시연회

    public override void Interact()
    {
        bool isWeared = PlayerScan.Instance.isWeared;
        bool eatMed = PlayerScan.Instance.eatMed;
        if(!isWeared && !eatMed)
        {
            StartCoroutine(DialogueManager.Instance.IContinueDialogue(7, 7));
        }
        else if(isWeared && !eatMed)
        {
            StartCoroutine(DialogueManager.Instance.IContinueDialogue(8, 8));
        }
        else if(isWeared && eatMed)
        {
            PlayerScan.Instance.transform.Find("bed").gameObject.GetComponent<AudioSource>().pitch = 3;
            GetComponent<SpriteRenderer>().sprite = sleepingJung;
            PlayerScan.Instance.GetComponent<SpriteRenderer>().sortingOrder = -1;
            StartCoroutine(GameManager.Instance.IFadeOut(5f));
            StartCoroutine(GameManager.Instance.ILoadScene("DreamMap", 5f, "DreamMap"));
            GameManager.Instance.gameSceneProcedure++;
        }
    }
}
