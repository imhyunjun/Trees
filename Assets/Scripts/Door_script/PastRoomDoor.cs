using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastRoomDoor : Door
{
    private void Update()
    {
        isOpened = PlayerScan.instance.progressStatus >= ProgressStatus.E_TalkWithTreeFirstTime;
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
