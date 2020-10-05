using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcohol : Item
{
    [SerializeField]
    private GameObject counter;
    [SerializeField]
    private GameObject marketOwner;

    public override void UseItem()
    {
        base.UseItem();
        gameObject.SetActive(true);
        gameObject.transform.SetParent(counter.transform);          //카운터 위에 올리기
        gameObject.transform.position = counter.transform.position;               //카운터 중간(나중에 확인)

        StartCoroutine(DialogueManager.instance.IShowDialogueBalloon(marketOwner, "chapter_4"));           //5천원 카드니?
    }
}
