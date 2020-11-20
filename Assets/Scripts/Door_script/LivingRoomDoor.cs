using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomDoor : Door
{
    [SerializeField]
    private Father father;
    [SerializeField]
    private Front front;
    [SerializeField]
    private CashCard cashCard;
    [SerializeField]
    private SpriteRenderer livingRoom;
    [SerializeField]
    private Sprite nightLivingRoomSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (status == ProgressStatus.E_ErrandFinished)
            {
                isOpened = false;
                DialogueManager.instance.PlayDialogue("chapter_0_18");               //나가기 싫어..잠이나 자자
            }
            else
            {
                isOpened = true;
            }
        }
    }

    public override void AfterPlayerArrived()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GiveBackMirrorToTree)
        {
            father.gameObject.SetActive(true);
            cashCard.gameObject.SetActive(true);
            livingRoom.sprite = nightLivingRoomSprite;
            //DialogueManager.instance.PlayDialogue("chapter_0");
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            GameObject player = GameManager.instance.player;
            list.Add(new KeyValuePair<GameObject, string>(player, "chapter_0_0"));
            list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_0_1"));
            list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_0_2"));
            list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_0_3"));
            list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_0_4"));
            DialogueManager.instance.ShowDialogueBallon(list);
            front.CanPass(false);                                         // 못나가게 막기
            PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithCurrentDad;
        }
    }

    public override void OnUseDoor()
    {
        BGMManager.instance.PlayBGM(BGM.LivingRoom);
    }
}
