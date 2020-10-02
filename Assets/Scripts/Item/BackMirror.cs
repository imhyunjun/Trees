using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMirror : Item
{
    public override void GetItem()
    {
        base.GetItem();
        PlayerScan.instance.progressStatus = ProgressStatus.E_GetBackMirror;
    }

    public override void UseItem()
    {
        base.UseItem();
        GameManager.instance.treeGrowStatus++; // 나무 성장
        StartCoroutine(GameManager.instance.ILoadScene(_sceneName: "Prologue", _fadetime: 4, _playerIn: "House"));
        PlayerScan.instance.progressStatus = ProgressStatus.E_GiveBackMirrorToTree;
    }
}
