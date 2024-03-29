﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : NPC
{
    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (status == ProgressStatus.E_Start || status == ProgressStatus.E_JungGotShocked)
        {
            DialogueManager.instance.PlayDialogue("prologue_5");
        }
        else if (status == ProgressStatus.E_ChangeClothes || status == ProgressStatus.E_ChangeClothes2)
        {
            SoundManager.PlaySFX("drawer");

            DialogueManager.instance.PlayDialogue("prologue_7", true);
            ButtonPanel.instance.SetUp(() =>
            {
                SoundManager.PlaySFX("re_water_gulp");
                if (status == ProgressStatus.E_ChangeClothes) PlayerScan.instance.progressStatus = ProgressStatus.E_EatMedicine;
                else if (status == ProgressStatus.E_ChangeClothes2) PlayerScan.instance.progressStatus = ProgressStatus.E_EatMedicine2;
            }, () =>
            {
                DialogueManager.instance.PlayDialogue("prologue_8");
            });
        }
        else if (status == ProgressStatus.E_ErrandFinished && Inventory.instance.IsPlayerHasItem(typeof(Cash)))
        {
            DialogueManager.instance.PlayDialogue("chapter_0_16", true);        //천원을 넣으시겠습니까?
            ButtonPanel.instance.SetUp(() =>
            {
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(GameManager.instance.player, "chapter_13")); // 이제 얼마나 모은거지..
                DialogueManager.instance.ShowDialogueBallon(list);
                SoundManager.PlaySFX("drawer");
                Inventory.instance.DeleteItemInSlot(ObjectManager.GetObject<Cash>()); // 인벤토리에서 천원 없어짐
            }, () =>
            {
                DialoguePanel.instance.Hide(0);
                ButtonPanel.instance.Hide();
            });
        }
        else if (status == ProgressStatus.E_Chapter2Start)
        {
            DialogueManager.instance.PlayDialogue("chapter_2_2");       //늦기전에 학교에 가자
        }
    }
}
