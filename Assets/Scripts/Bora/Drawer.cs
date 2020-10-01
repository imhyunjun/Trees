using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : NPC
{
    public override void Interact()
    {
        bool isWeared = PlayerScan.Instance.isWeared;
        bool eatMed = PlayerScan.Instance.eatMed;
        if (!isWeared && !eatMed)
        {
            StartCoroutine(DialogueManager.Instance.IContinueDialogue(7, 7));
        }
        else if (isWeared && !eatMed)
        {
            PlayerScan.Instance.transform.Find("drawer").gameObject.GetComponent<AudioSource>().pitch = 2;
            DialogueManager.Instance.OnOffDialogueImage(true); //yes, no 버튼 있을 경우 반복문 사용 금지. stack에 쌓여서 못 빠져 나옴.
            StartCoroutine(DialogueManager.Instance.PlayText(9));

            ButtonPanelTemp.Instance.SetUp(() =>
            {
                PlayerScan.Instance.eatMed = true;
                GameObject.Find("water-gulp").GetComponent<AudioSource>().Play();
            }, () =>
            {
                StartCoroutine(DialogueManager.Instance.PlayText(10));
            });
        }
    }
}
