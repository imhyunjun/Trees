using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomDoor : Door
{
    [SerializeField]
    private Father father;

    private void Awake()
    {
        isOpened = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (status == ProgressStatus.E_PayedDone)
        {
            //isOpened = false;
            if (Inventory.instance.IsPlayerHasItem("검은봉투", "카드"))
            {
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_8"));   // 술은?
                DialogueManager.instance.ShowDialogueBallon(list);
                //StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_12"));
            }
            else if (Inventory.instance.IsPlayerHasItem("술"))              //술 안줬으면 술은?   , 두개 합친느건 나중에 생각
            {
                //StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_9"));
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_9"));   // 술은?
                DialogueManager.instance.ShowDialogueBallon(list);
            }
            else if (Inventory.instance.IsPlayerHasItem("카드"))                 //카드를 안줬으면 카드는?
            {
                //StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_10"));
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_10"));   // 카드는?
                DialogueManager.instance.ShowDialogueBallon(list);
            }
        }

    }

    public override void OnUseDoor()
    {
        BGMManager.instance.PlayBGM(BGM.JungRoom);
    }
}