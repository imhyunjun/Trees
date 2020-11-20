using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : Item                //일단 카드로 생각하고 했어요
{
    [SerializeField]
    private Sprite plasticbagSprite;        //검은봉투스프라이트
    [SerializeField]
    private GameObject marketOwner;
    [SerializeField]
    private Alcohol alchol;
    [SerializeField]
    private Father father;
    [SerializeField]
    private Transform table;
    [SerializeField]
    private RoomDoor roomdoor;

    public override void UseItem()
    {
        base.UseItem();
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetAlcholBottle)         //술병을 얻었을 때만
        {
            //클릭사운드 추가
            List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
            list.Add(new KeyValuePair<GameObject, string>(marketOwner, "chapter_5_0"));   // 계산 다됐어
            list.Add(new KeyValuePair<GameObject, string>(marketOwner, "chapter_5_1"));   // 너네 아빠보고 술좀 그만하라고 해라
            SoundManager.PlaySFX("card_reader");
            DialogueManager.instance.ShowDialogueBallon(list, 0.6f, 4.5f);
            alchol.GetComponent<SpriteRenderer>().sprite = plasticbagSprite;                   //술 스프라이트 -> 검은봉투로 변경 일단 동시에
            Item item = alchol.GetComponent<Item>();
            item.itemName = "검은봉투";
            item.itemSprite = plasticbagSprite;
            Inventory.instance.ReUseItem(true, gameObject);
            PlayerScan.instance.progressStatus = ProgressStatus.E_PayedDone;
        }
        else if (PlayerScan.instance.progressStatus == ProgressStatus.E_PayedDone)
        {
            //놓는 사운드 추가
            gameObject.SetActive(true);
            gameObject.transform.SetParent(table); 
            Vector3 tempvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempvec.z = 0;
            gameObject.transform.position = tempvec;
            if (Inventory.instance.IsPlayerDoesntHaveItem("검은봉투"))
            {
                List<KeyValuePair<GameObject, string>> list = new List<KeyValuePair<GameObject, string>>();
                list.Add(new KeyValuePair<GameObject, string>(father.gameObject, "chapter_7"));   // 네 방으로 들어가
                DialogueManager.instance.ShowDialogueBallon(list);
                //DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_11");              // 네 방으로 들어가
                PlayerScan.instance.progressStatus = ProgressStatus.E_ErrandFinished;
                roomdoor.isOpened = true;
            }
        }
    }
}