using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingRoomDoor : Door
{
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
                DialogueManager.instance.PlayDialogue("chapter_0_18");               //나가기 싫어..잠이나 자자
            }
            else if(GameManager.CheckCondition(ProgressStatus.E_Chapter2Start, PlayerAnim.E_Pajama))
            {
                DialogueManager.instance.PlayDialogue("chpater_2_1");               //학교에 가려면 옷을 입어야 해
            }
        }
    }

    public override void AfterPlayerArrived()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GiveBackMirrorToTree)
        {
            Father father = ObjectManager.GetObject<Father>();
            father.gameObject.SetActive(true);
            ObjectManager.GetObject<CashCard>().gameObject.SetActive(true);
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
            ObjectManager.GetObject<Front>().CanPass(false);                                         // 못나가게 막기
            PlayerScan.instance.progressStatus = ProgressStatus.E_TalkWithCurrentDad;
        }
    }

    public override void OnUseDoor()
    {
        BGMManager.instance.PlayBGM(BGM.LivingRoom);
    }
}
