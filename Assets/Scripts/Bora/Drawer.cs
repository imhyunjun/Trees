using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : NPC
{
    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if (status == ProgressStatus.E_Start)
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(7, 7));
        }
        else if (status == ProgressStatus.E_ChangeClothes)
        {
            PlayerScan.instance.transform.Find("drawer").gameObject.GetComponent<AudioSource>().pitch = 2;
            DialogueManager.instance.OnOffDialogueImage(true); //yes, no 버튼 있을 경우 반복문 사용 금지. stack에 쌓여서 못 빠져 나옴.
            StartCoroutine(DialogueManager.instance.PlayText(9));

            ButtonPanelTemp.instance.SetUp(() =>
            {
                PlayerScan.instance.progressStatus++;
                GameObject.Find("water-gulp").GetComponent<AudioSource>().Play();
            }, () =>
            {
                StartCoroutine(DialogueManager.instance.PlayText(10));
            });
        }
    }
}
