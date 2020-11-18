using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : Door
{
    [SerializeField]
    private Father father;

    private void Awake()
    {
        isOpened = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (status == ProgressStatus.E_PayedDone)
        {
            isOpened = false;
            if (Inventory.instance.IsPlayerHasItem("술", "카드"))
            {
                StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_8"));
                Debug.Log("1");
            }
            else if (Inventory.instance.IsPlayerHasItem("술"))              //술 안줬으면 술은?   , 두개 합친느건 나중에 생각
            {
                Debug.Log("2");
                StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_9"));

            }
            else if (Inventory.instance.IsPlayerHasItem("카드"))                 //카드를 안줬으면 카드는?
            {
                Debug.Log("3");
                StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_9"));

            }
        }
        else
            isOpened = true;
    }
}