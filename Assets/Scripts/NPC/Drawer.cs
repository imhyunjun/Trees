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
            SoundManager.PlaySFX("drawer");
            DialoguePanel.instance.Show();
            StartCoroutine(DialogueManager.instance.PlayText(9));
            ButtonPanel.instance.SetUp(() =>
            {
                SoundManager.PlaySFX("re_water_gulp");
                PlayerScan.instance.progressStatus = ProgressStatus.E_EatMedicine;
            }, () =>
            {
                StartCoroutine(DialogueManager.instance.PlayText(10));
            });
        }
    }
}
