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

  //  public int increaseDialogueStart;       //나무와의 대화를 관리할 숫자
   // public int increaseDialogueEnd;         //나무와의 대화를 관리할 숫자
   // public int afterGiveTreeStart;          //나무한테 아이템 주고 난 후 다이얼로그 시작지점
   // public int afterGiveTreeEnd;            //나무한테 아이템 주고 난 후 다이얼로그 끝지점

    private void Awake()
    {
        isInInventory = false;
        itemSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public virtual void GetItem() // 해당 아이템을 얻었을 때 실행되는 함수
    {
        isInInventory = true;
    }

    public virtual void UseItem() // 해당 아이템을 사용했을 때 실행되는 함수
    {
        isInInventory = false;
    }
}
