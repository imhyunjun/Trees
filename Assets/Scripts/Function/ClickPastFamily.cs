using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPastFamily : MonoBehaviour
{
    [SerializeField]
    private GameObject backMirror;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if (!hit) return;
            if (PlayerScan.instance.progressStatus == ProgressStatus.E_Sleep && hit.collider.name == "PastMother")
            {
                StartCoroutine(DialogueManager.instance.IContinueDialogue(25, 27));
                PlayerScan.instance.progressStatus++;
            }
            else if (PlayerScan.instance.progressStatus == ProgressStatus.E_TalkWithPastMom && hit.collider.name == "PastJung")
            {
                StartCoroutine(DialogueManager.instance.IContinueDialogue(28, 30));
                PlayerScan.instance.progressStatus++;
            }
            else if (PlayerScan.instance.progressStatus == ProgressStatus.E_TalkWithPastJung && hit.collider.name == "PastFather")
            {
                StartCoroutine(DialogueManager.instance.IContinueDialogue(31, 33));
                StartCoroutine(SettingActive());
                PlayerScan.instance.progressStatus++;
            }
        }
    }

    IEnumerator SettingActive()
    {
        yield return StartCoroutine(DialogueManager.instance.IContinueDialogue(31, 33));   //아빠 대화 끝나고 
        yield return new WaitForSeconds(1.5f);                                                              //1.5초 후에
        AudioSource audio = SoundManager.PlaySFX("Siren", true);
        backMirror.SetActive(true);  // 백미러등장
        yield return new WaitForSeconds(1.5f);                                                              //임시로 끝나는거 설정
        audio.Stop();
        PlayerScan.instance.progressStatus++;
    }
}

