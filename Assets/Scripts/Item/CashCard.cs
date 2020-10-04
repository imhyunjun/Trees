using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashCard : Item                //일단 카드로 생각하고 했어요
{
    [SerializeField]
    private Front front;
    [SerializeField]
    private FrontDoor frontDoor;
    [SerializeField]
    private GameObject alcohol;             //술병
    [SerializeField]
    private Sprite plasticbagSprite;        //검은봉투스프라이트

    public override void GetItem()
    {
        base.GetItem();
        front.CanPass(true);
        frontDoor.isOpened = true;                              // 현관문 열림
        PlayerScan.instance.progressStatus = ProgressStatus.E_GetCashCard;
    }

    public override void UseItem()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetAlcholBottle)         //술병을 얻었을 때만
        {
            base.UseItem();
            StartCoroutine(UseCard());    
        }
    }

    private IEnumerator UseCard()
    {
        yield return new WaitForSeconds(0.5f);
        //클릭사운드 추가
        StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(null, "chapter_5"));   //말풍선.. 근데 두개 넘어가는 방식 알려주시면 수정
        alcohol.GetComponent<SpriteRenderer>().sprite = plasticbagSprite;                   //술 스프라이트 -> 검은봉투로 변경 일단 동시에
        PlayerScan.instance.progressStatus = ProgressStatus.E_PayedDone;
    }
}