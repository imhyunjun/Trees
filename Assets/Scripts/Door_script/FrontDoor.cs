using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontDoor : Door
{
    private void Start()
    {
        isOpened = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        //if(status == ProgressStatus.E_PayedDone)        //물건을 다 사고 집에 들어오면 못나감
        //{
        //    isOpened = false;
        //}
        if (!Inventory.instance.IsPlayerDoesntHaveItem("술", "카드") && status == ProgressStatus.E_PayedDone)
        //오브젝트 이름은 바뀌면 바꿔주세요 일단 Card, 인벤토리 알코올 or 카드가 있으면
        {
            // StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_12"));   //어딜 가는거야?
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            list.Add(new KeyValuePair<GameObject, string>(ObjectManager.GetObject<Father>().gameObject, "chapter_8"));   //어딜 가는거야?
            DialogueManager.instance.ShowDialogueBallon(list);
        }
        else if(status == ProgressStatus.E_ErrandFinished)
        {
            DialogueManager.instance.PlayDialogue("chapter_0_15");  //방으로 들어가자
        }
        else if(status == ProgressStatus.E_JungGotShocked)
        {
            DialogueManager.instance.PlayDialogue("chapter_2_5");   //기분이 너무 안좋아 잠이나 자자.
        }
    }

    public override void OnUseDoor()
    {
        PlayerMove.canMove = false;
    }
    public override void AfterPlayerArrived()
    {
        GameManager.instance.player.GetComponent<SpriteRenderer>().flipX = false;
        Invoke("CanMove", 0.5f); // 강제로 못움직이게하고 0.5초뒤에 움직일 수 있게 해줌
    }

    private void CanMove()
    {
        PlayerMove.canMove = true;
    }

}
