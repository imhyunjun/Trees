using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Sprite slotDefaultSprite;        //슬록 기본 이미지
    public Sprite hasItemSprite;            //슬롯이 가지고 있는 아이템 이미지
    Color slotColor;                 //아이템 투명도 조절(클릭)
    public Color tempColor;                 //임시 칼라 이것으로 직접 조절
    public bool isSlotHasItem;              //슬롯에 아이템이 있는지
    public GameObject hasItem;              //가지고 있는 아이템 이름

    private void Awake()
    {
        slotDefaultSprite = gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        isSlotHasItem = false;
        hasItemSprite = null;
        hasItem = null;
        slotColor = gameObject.transform.GetChild(0).GetComponent<Image>().color;
        tempColor = slotColor;
    }

    private void Update()
    {
        if(isSlotHasItem)                                   
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = hasItemSprite;      //아이템 가지고 있으면 아이템 이미지
        }
        else
        {
            gameObject.transform.GetChild(0).GetComponent<Image>().sprite = slotDefaultSprite;  //없으면 기본 이미지
        }

        slotColor = tempColor;
        gameObject.transform.GetChild(0).GetComponent<Image>().color = slotColor;
    }
}
