using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomDoor : Door
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (status == ProgressStatus.E_GiveBackMirrorToTree)
            {
                GameManager.GetObject<Father>().gameObject.SetActive(true);
                GameManager.GetObject<Card>().gameObject.SetActive(true);
                GameManager.GetObject<Cash>().gameObject.SetActive(true);
                DialogueManager.instance.PlayDialogue("chapter_0");
                GameManager.GetObject<Front>().CanPass(false);                                         // 못나가게 막기
                PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithCurrentDad;
            }
            else if (status == ProgressStatus.E_ErrandFinished)
            {
                isOpened = false;
                DialogueManager.instance.PlayDialogue("chapter_14");               //나가기 싫어..잠이나 자자
            }
            else
            {
                isOpened = true;
            }
        }
    }
}
