using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButton : MonoBehaviour
{
    public GameObject inventoryButton;
    RectTransform inventoryRect;
    public float inventoryMoveDistance;
    bool isinventoryOpend;

    private void Awake()
    {
        isinventoryOpend = false;
        inventoryRect = inventoryButton.GetComponent<RectTransform>();

    }
    public void OnClickInventoryButton()
    {
        gameObject.GetComponent<AudioSource>().Play();

        if(!isinventoryOpend)
        {
            inventoryRect.Translate(inventoryMoveDistance * Vector3.left);
            isinventoryOpend = true;
        }
        else
        {
            inventoryRect.Translate(inventoryMoveDistance * Vector3.right);
            isinventoryOpend = false;
        }
    }
}
