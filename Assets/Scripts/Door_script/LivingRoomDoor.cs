using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomDoor : Door
{
    [SerializeField]
    private Father father;
    [SerializeField]
    private Card card;
    [SerializeField]
    private Cash cash;
    [SerializeField]
    private Front front;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (status == ProgressStatus.E_GiveBackMirrorToTree)
            {
                father.gameObject.SetActive(true); 
                card.gameObject.SetActive(true);
                cash.gameObject.SetActive(true);
                StartCoroutine(DialogueManager.instance.IContinueDialogue("chapter_0"));
                front.CanPass(false);                                         // 못나가게 막기
                PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithCurrentDad;
            }
           
            else
            {
                isOpened = true;
            }
        }
    }
}
