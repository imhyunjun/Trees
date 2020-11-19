using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Front : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D[] coliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            ProgressStatus status = PlayerScan.instance.progressStatus;
            if (status == ProgressStatus.E_Start)
                DialogueManager.instance.PlayDialogue("prologue_3"); // 처음 들어올 때는 방으로 들어가자고 말함
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ProgressStatus status = PlayerScan.instance.progressStatus;
            if (status == ProgressStatus.E_Start)  // 다시 못나가게 경계 생김
                CanPass(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ProgressStatus status = PlayerScan.instance.progressStatus;
            if (status < ProgressStatus.E_Sleep) // 다시 나가려고 하면 아빠가 오기전에 방으로 들어가자고 말함
                DialogueManager.instance.PlayDialogue("prologue_4");
            else if (status == ProgressStatus.E_TalkWithCurrentDad)
                DialogueManager.instance.PlayDialogue("chapter_0_5"); // 카드를 챙기자고 말함
        }
    }

    public void CanPass(bool can)
    {
        for (int i = 0; i < coliders.Length; i++)
            coliders[i].isTrigger = can;
    }
}