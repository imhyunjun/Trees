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

    public override void UseItem()
    {
        base.UseItem();
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetAlcholBottle)         //술병을 얻었을 때만
        {
            //클릭사운드 추가
            StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(marketOwner, "chapter_0_5"));   //말풍선.. 근데 두개 넘어가는 방식 알려주시면 수정
            alchol.GetComponent<SpriteRenderer>().sprite = plasticbagSprite;                   //술 스프라이트 -> 검은봉투로 변경 일단 동시에
            Inventory.instance.ReUseItem(true, gameObject);
            PlayerScan.instance.progressStatus = ProgressStatus.E_PayedDone;
        }
        else if (PlayerScan.instance.progressStatus == ProgressStatus.E_PayedDone)
        {
            //놓는 사운드 추가
            gameObject.SetActive(true);
            gameObject.transform.SetParent(null);   //부모 해제
            Vector3 tempvec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            tempvec.z = 0;
            gameObject.transform.position = tempvec;
            if (Inventory.instance.IsPlayerDoesntHaveItem("술"))
            {
                DialogueManager.instance.IShowDialogueBalloon(father.gameObject, "chapter_0_7");              // 네 방으로 들어가
                PlayerScan.instance.progressStatus = ProgressStatus.E_ErrandFinished;
            }
        }
    }
}