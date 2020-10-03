using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomDoor : Door
{
    [SerializeField]
    private Father father;
    [SerializeField]
    private CashCard cashCard;
    [SerializeField]
    private Front front;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (PlayerScan.instance.progressStatus == ProgressStatus.E_GiveBackMirrorToTree)
            {
                father.gameObject.SetActive(true); 
                cashCard.gameObject.SetActive(true);

                StartCoroutine(DialogueManager.instance.IContinueDialogue("chapter_0"));
                front.CanPass(false);                                         // 못나가게 막기
                PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithCurrentDad;
            }
        }
    }
}
