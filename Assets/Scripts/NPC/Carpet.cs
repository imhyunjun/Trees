using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carpet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(PlayerScan.instance.progressStatus == ProgressStatus.E_PayedDone)            //계산이 다 끝난 후라면
        {
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(null, "chapter_6"));               //나중에 null 대신 아빠
        }
    }
}
