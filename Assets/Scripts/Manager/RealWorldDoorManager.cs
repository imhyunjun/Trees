using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealWorldDoorManager : MonoBehaviour
{
    private static RealWorldDoorManager instanced;
    public static RealWorldDoorManager Instance => instanced;

    [SerializeField]
    private Door[] realWorlddoors;

    private void Awake()
    {
        if (instanced == null)
        {
            instanced = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 문 열고 닫기
    /// </summary>
    /// <param name="door"></param>  열고 닫을 문
    /// <param name="status"></param> true == 엶, false == 닫음
    public void OpenCloseDoor(System.Type _type, bool _status)
    {
        foreach (Door door in realWorlddoors)
        {
            if (door.GetType() == _type)
            {
                door.isOpened = _status;
                break;
            }
        }
    }


}
