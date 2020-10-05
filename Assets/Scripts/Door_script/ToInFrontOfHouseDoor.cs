using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToInFrontOfHouseDoor : Door
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            isOpened = true;
        }
    }
}
