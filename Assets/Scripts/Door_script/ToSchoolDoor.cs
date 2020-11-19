﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSchoolDoor : Door
{
    private void Start()
    {
        isOpened = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashNCard)
            DialogueManager.instance.PlayDialogue("chapter_0_6");  // 우선 슈퍼에 가야해
    }
}
