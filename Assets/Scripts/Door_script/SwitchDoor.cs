using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoor : MonoBehaviour
{
    public List<Door> doors = new List<Door>(); //Door 클래스 리스트 생성

    public void AddDoor(Door _door)
    {
        if (doors.Contains(_door)) //리스트에 이미 추가할 문이 있는 경우
        {
            Debug.Log("이미 리스트에 있어 추가 불가."); //유니티 로그 창에 띄움. 
        }
        else
        {
            doors.Add(_door); //그 외에는 리스트에 그냥 추가
        }
    }

    public void RemoveDoor(Door _door) //Door 클래스 제거
    {
        if (doors.Contains(_door)) //리스트에 있는 경우 제거
        {
            doors.Remove(_door);
        }
        else
        {
            Debug.Log("이미 리스트에 없어 제거 불가."); // 없는 경우 유니티 로그 창에 띄움.
        }
    }

    public void OnClickSwitchDoor() //문을 여닫을 때 쓰는 메소드
    {
        NotifyDoors(); //해당 메소드 아래에 선언
    }

    public void NotifyDoors() //지정한 문을 찾아 여닫는 메소드
    {
        foreach (Door _d in doors) //doors(리스트)에서 문을 찾음
        {
            //_d.OpenOrClose(); //찾은 후에 문을 여닫음.
        }
    }
}
