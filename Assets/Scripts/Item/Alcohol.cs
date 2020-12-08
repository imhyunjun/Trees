using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcohol : Item
{
    [SerializeField]
    private GameObject counter;
    [SerializeField]
    private GameObject marketOwner;
    [SerializeField]
    private Transform table;

    public override void GetItem()
    {
        base.GetItem();
        if(PlayerScan.instance.progressStatus == ProgressStatus.E_GetAlcholBottle)
            SoundManager.PlaySFX("bottle");                             // 병소리 나면서 획득
    }

    public override void UseItem()
    {
        base.UseItem();

        if (PlayerScan.instance.progressStatus == ProgressStatus.E_PayedDone)
        {
            //놓는 사운드 추가
            gameObject.SetActive(true);
            gameObject.transform.SetParent(table);
            Vector3 tempvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempvec.z = 0;
            gameObject.transform.position = tempvec;
            if (Inventory.instance.IsPlayerDoesntHaveItem("카드"))
            {
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(ObjectManager.GetObject<Father>().gameObject, "chapter_7"));   // 네 방으로 들어가
                DialogueManager.instance.ShowDialogueBallon(list);
                PlayerScan.instance.progressStatus = ProgressStatus.E_ErrandFinished;
                ObjectManager.GetObject<RoomDoor>().isOpened = true;
            }
        }
        else if(PlayerScan.instance.progressStatus == ProgressStatus.E_GetAlcholBottle)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetParent(counter.transform);
            gameObject.transform.position = counter.transform.position;                                         //카운터 중간(나중에 확인)         
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            list.Add(new KeyValuePair<GameObject, string>(marketOwner, "chapter_4")); //5천원 카드니?
            DialogueManager.instance.ShowDialogueBallon(list, 0.6f, 4.5f); 
        }
    }
}
