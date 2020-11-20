using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSuperMarketDoor : Door
{
    [SerializeField]
    private GameObject marketOwner;

    public override void AfterPlayerArrived()
    {
        GameManager.instance.player.transform.localScale = new Vector3(-1f, 1f, 1f);
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashNCard)
        {
            //StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(marketOwner, "chapter_0_7"));
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            list.Add(new KeyValuePair<GameObject, string>(marketOwner, "chapter_3"));
            DialogueManager.instance.ShowDialogueBallon(list, 0.6f, 4.5f);
        }
    }

}
