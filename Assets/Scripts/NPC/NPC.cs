using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class NPC : MonoBehaviour
{
    public abstract void Interact();   
}