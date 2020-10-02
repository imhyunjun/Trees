using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRoomDoor : Door
{
    private void Start()
    {
        isOpened = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") )
        {

        }
    }

    public void Open(GameObject _door) //문 열기 구현
    {
        isOpened = false;
        _door.SetActive(false);
    }

    public void Close(GameObject _door) //문 닫기 구현
    {
        isOpened = true;
        _door.SetActive(true);

    }
    
}
