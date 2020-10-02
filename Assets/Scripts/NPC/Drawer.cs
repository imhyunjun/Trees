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
            StartCoroutine(DialogueManager.instance.IContinueDialogue("prologue_5"));
        }
        else if (status == ProgressStatus.E_ChangeClothes)
        {
            SoundManager.PlaySFX("drawer");
            DialoguePanel.instance.Show();
            StartCoroutine(DialogueManager.instance.PlayText("prologue_7"));
            ButtonPanel.instance.SetUp(() =>
            {
                PlayerScan.instance.progressStatus++;
                SoundManager.PlaySFX("re_water_gulp");
            }, () =>
            {
                StartCoroutine(DialogueManager.instance.PlayText("prologue_8"));
            });
        }
    }
}
