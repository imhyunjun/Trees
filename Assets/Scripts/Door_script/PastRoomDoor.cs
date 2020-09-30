using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRoomDoor : Door
{
    private void Awake()
    {
        isOpened = true;
    }

    //public override void OpenOrClose(GameObject _gameObject, bool isClosed) //문을 여닫는 메소드 구현
    //{
    //    //bool dialogueActive = dialogueManager.GetDialogueActive();

    //    //if (dialogueActive==false) //대화창이 안 뜬 상태에만 문이 열려있음.
    //    //{
    //    if (isClosed)
    //    {
    //        Open(_gameObject);
    //    }
        
    //    //}

    //    // else if(dialogueActive==true) // 대화창이 뜬 상태에서는 문이 닫혀있음. 
    //    //{
    //    if (!isClosed)
    //    {
    //        Close(_gameObject);
    //    }

    //    //}
    //}

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
