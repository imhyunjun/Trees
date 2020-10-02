using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : NPC
{
    GameObject alcholBottle;                                //임시용

    public override void Interact()
    {
        Inventory.instance.GetItemInSlot(alcholBottle);
    }
}
