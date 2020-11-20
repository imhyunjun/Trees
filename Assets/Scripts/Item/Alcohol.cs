using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcohol : Item
{
    [SerializeField]
    private GameObject counter;
    [SerializeField]
    private Father father;
    [SerializeField]
    private GameObject marketOwner;
    [SerializeField]
    private Transform table;
    [SerializeField]
    private RoomDoor roomdoor;

    public override void GetItem()
    {
        base.GetItem();
        SoundManager.PlaySFX("bottle"); // 병소리 나면서 획득
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
                //DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_12");              // 네 방으로 들어가
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_7"));   // 네 방으로 들어가
                DialogueManager.instance.ShowDialogueBallon(list);
                PlayerScan.instance.progressStatus = ProgressStatus.E_ErrandFinished;
                roomdoor.isOpened = true;

            }
        }
        else if(PlayerScan.instance.progressStatus == ProgressStatus.E_GetAlcholBottle)
        {
            gameObject.SetActive(true);
            gameObject.transform.SetParent(counter.transform);
            gameObject.transform.position = counter.transform.position;                                         //카운터 중간(나중에 확인)
            //StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(marketOwner, "chapter_0_8"));           
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            list.Add(new KeyValuePair<GameObject, string>(marketOwner, "chapter_4")); //5천원 카드니?
            DialogueManager.instance.ShowDialogueBallon(list, 0.6f, 4.5f); 
        }
    }
}
