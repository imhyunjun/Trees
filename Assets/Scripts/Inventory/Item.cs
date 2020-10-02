using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string itemName;                
    public bool isInInventory;
    public Sprite itemSprite;
    public string canInteractWith;          //상호작용할 수 있는 오브젝트 이름

    public int increaseDialogueStart;       //나무와의 대화를 관리할 숫자
    public int increaseDialogueEnd;         //나무와의 대화를 관리할 숫자
    public int afterGiveTreeStart;          //나무한테 아이템 주고 난 후 다이얼로그 시작지점
    public int afterGiveTreeEnd;            //나무한테 아이템 주고 난 후 다이얼로그 끝지점

    public int changeTree;                  //나무 상태 변화 시키는 것은 1 나머진 0;

    private void Awake()
    {
        //itemName = gameObject.name;
        isInInventory = false;
        itemSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }
}
