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

    private void Awake()
    {
        isInInventory = false;
        itemSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public virtual void GetItem()
    {
        isInInventory = true;
    }

    public virtual void UseItem()
    {
        isInInventory = false;
    }
}
