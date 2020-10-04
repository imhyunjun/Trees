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
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (status == ProgressStatus.E_GiveBackMirrorToTree)
            {
                father.gameObject.SetActive(true); 
                cashCard.gameObject.SetActive(true);

                StartCoroutine(DialogueManager.instance.IContinueDialogue("chapter_0"));
                front.CanPass(false);                                         // 못나가게 막기
                PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithCurrentDad;
            }
            else if (status == ProgressStatus.E_PayedDone)
            {
                isOpened = false;
                if (Inventory.instance.IsPlayerHasItem("Alcohol", "Card"))
                {
                    DialogueManager.instance.IShowDialogueBalloon(null, "chapter_8");
                }
                else if(Inventory.instance.IsPlayerHasItem("Alcohol"))              //술 안줬으면 술은?   , 두개 합친느건 나중에 생각
                {
                    DialogueManager.instance.IShowDialogueBalloon(null, "chapter_9");
                }
                else if(Inventory.instance.IsPlayerHasItem("Card"))                 //카드를 안줬으면 카드는?
                {
                    DialogueManager.instance.IShowDialogueBalloon(null, "chapter_10");
                }
            }
            else
            {
                isOpened = true;
            }
        }
    }
}
