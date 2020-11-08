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
        StartCoroutine(UseBackMirror());
    }

    private IEnumerator UseBackMirror()
    {
        DialogueManager.instance.PlayDialogue("prologue_16");
        yield return new WaitUntil(()=> DialogueManager.instance.playDialogueCor == null);  // 대화 끝날 때 까지 기다린 후

        GameManager.instance.treeGrowStatus++;     // 나무 성장 
        PlayerScan.instance.progressStatus = ProgressStatus.E_GiveBackMirrorToTree;

        PlayerMove.canMove = false;
        GameManager.instance.StartLoadSceneCor("Prologue", 4f, 3f, "House", () =>
        {
            Transform bed = FindObjectOfType<Bed>().transform;
            Transform jungsRoom = bed.parent;
            GameManager.instance.player.transform.position = new Vector3(bed.position.x, bed.position.y, 0);        // 플레이어 위치 침대
            Camera.main.transform.position = new Vector3(jungsRoom.position.x, jungsRoom.position.y, -10);       // 카메라 위치 정의 방

            // 일어나는 애니메이션
            PlayerMove.canMove = true;
        });
    }
}