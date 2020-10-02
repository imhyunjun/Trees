using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Front : MonoBehaviour
{
    [SerializeField]
    private BoxCollider2D[] coliders;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(5, 5)); // 처음 들어올 때는 방으로 들어가자고 말함
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            for (int i = 0; i < coliders.Length; i++)  // 방으로 들어가면 다시 못나가게 경계 생김
                coliders[i].isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 다시 나가려고 하면 아빠가 오기전에 방으로 들어가자고 말함
        {
            StartCoroutine(DialogueManager.instance.IContinueDialogue(6, 6));
        }
    }
}
