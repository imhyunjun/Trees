using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour //상속을 위한 문 추상 클래스, 이 클래스를 상속받아 여러 문을 만들 수 있음.
{
   // public abstract void OpenOrClose(GameObject gameObject, bool aa);             //나중에 각 문마다 오픈 클로즈 조건 달게 하게끔

    public GameObject arrivePoint;          //각 문마다 도착지점
    public GameObject cameraArrivePoint;    //각 문마다 카메라가 이동하는 지점 - 각 배경의 좌표
    public string destinationName;          //문마다 목적지 이름 - 발자국 관리용
    public bool isOpened = true;            //각 문마다 오픈 상태
    public bool playSfx = true; // 문 열리는 효과음 나는지

    public virtual void OnUseDoor()
    {

    }

    public virtual void AfterPlayerArrived()
    {
        
    }
}
