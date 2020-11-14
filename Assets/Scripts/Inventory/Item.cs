using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public enum UseType // 아이템 사용 타입
    {
        Interact, // 어떤 대상과 상호작용하면서 사용
        Immediately // 그냥 클릭하면 즉시 사용
    }

    public string itemName;
    public bool isInInventory;
    public Sprite itemSprite;
    public UseType useType;
    public string canInteractWith;          //상호작용할 수 있는 오브젝트 이름

    private void Awake()
    {
        isInInventory = false;
        itemSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public virtual bool CanUse() // 현재 해당 아이템을 사용할 수 있는지를 반환하는 함수
    {
        return true;
    }

    public virtual void GetItem() // 아이템을 얻었을 때 실행되는 함수
    {
        isInInventory = true;
    }

    public virtual void UseItem()  // 아이템을 사용했을 때 실행되는 함수
    {
        isInInventory = false;
    }
    public virtual void FailToUse() // 아이템 사용에 실패했을 때 실행되는 함수
    {

    }
}
