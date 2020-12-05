using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : Door
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (status == ProgressStatus.E_PayedDone)
        {
            Father father = ObjectManager.GetObject<Father>();
            if (Inventory.instance.IsPlayerHasItem("검은봉투", "카드"))
            {
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_8"));   // 어딜 가는 거야?
                DialogueManager.instance.ShowDialogueBallon(list);
            }
            else if (Inventory.instance.IsPlayerHasItem("술"))              //술 안줬으면 술은?   , 두개 합친느건 나중에 생각
            {
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_9"));   // 술은?
                DialogueManager.instance.ShowDialogueBallon(list);
            }
            else if (Inventory.instance.IsPlayerHasItem("카드"))                 //카드를 안줬으면 카드는?
            {
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_10"));   // 카드는?
                DialogueManager.instance.ShowDialogueBallon(list);
            }
        }
        else if (status == ProgressStatus.E_ErrandFinished)
            ObjectManager.GetObject<LivingRoomDoor>().isOpened = false;
    }

    public override void OnUseDoor()
    {
        BGMManager.instance.PlayBGM(BGM.JungRoom);
    }
}