using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastFather : NPC
{
    [SerializeField]
    private GameObject backMirror;

    public override void Interact()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_TalkWithPastJung)
        {
            //DialogueManager.instance.PlayDialogue("prologue_14");
            StartCoroutine(TalkWithFather());
            PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithPastDad;
        }
    }

    IEnumerator TalkWithFather()
    {
        DialogueManager.instance.PlayDialogue("prologue_14");
        yield return new WaitUntil(() => DialogueManager.instance.playDialogueCor == null);             //아빠 대화 끝나고 
        yield return new WaitForSeconds(1.5f);                                                              //1.5초 후에
        AudioSource audio = SoundManager.PlaySFX("Siren", true);
        backMirror.SetActive(true);  // 백미러등장
        yield return new WaitForSeconds(1.5f);                                                              //임시로 끝나는거 설정
        if (audio != null) audio.Stop();
    }
}
