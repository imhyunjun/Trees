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
            DialogueManager.instance.PlayDialogue("prologue_5");
        }
        else if (status == ProgressStatus.E_ChangeClothes)
        {
            SoundManager.PlaySFX("drawer");

            DialogueManager.instance.PlayDialogue("prologue_7");
            ButtonPanel.instance.SetUp(() =>
            {
                SoundManager.PlaySFX("re_water_gulp");
                PlayerScan.instance.progressStatus = ProgressStatus.E_EatMedicine;
            }, () =>
            {
                DialogueManager.instance.PlayDialogue("prologue_8");
            });
        }
        else if (status == ProgressStatus.E_ErrandFinished)
        {
            DialogueManager.instance.PlayDialogue("chapter_12");        //천원을 넣으시겠습니까?
            ButtonPanel.instance.SetUp(() =>
            {
                //효과음?
                StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(null, "chapter_13"));          //이제 얼마나 모은거지..
            }, null);
        }
    }
}
