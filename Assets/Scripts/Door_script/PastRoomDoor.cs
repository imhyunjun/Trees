using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRoomDoor : Door
{
    private void Update()
    {
        isOpened = PlayerScan.instance.progressStatus >= ProgressStatus.E_TalkWithTreeFirstTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isOpened)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue("prologue_17"));   // 아직 들어갈 수 없다
        }
    }
}
