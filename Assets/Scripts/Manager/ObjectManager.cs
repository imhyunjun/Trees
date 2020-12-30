using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private Transform _map;
    [SerializeField]
    private GameObject[] _objects;

    private static Dictionary<string, GameObject> _mapDicinString;
    private static ObjectManager _instance;
    private static Dictionary<Type, GameObject> _objectDic;
    
    public static ObjectManager instance => _instance;

    public List<Type> doorsL = new List<Type>();
    public List<bool> doorsB
    {
        get
        {
            List<bool> result = new List<bool>();
            for(int i = 0; i < doorsL.Count; i++)
                result.Add(_objectDic[doorsL[i]].GetComponent<Door>().isOpened);
            return result;
        }
    }

    private Transform dreamBackGround;
    private Transform realBackGround;
  
    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        _objectDic = new Dictionary<Type, GameObject>(); // 클래스별로 딕셔너리에 저장
        _mapDicinString = new Dictionary<string, GameObject>();

        dreamBackGround = _map.transform.GetChild(0).GetChild(0);
        realBackGround = _map.transform.GetChild(1).GetChild(0);

        Item[] items = _map.GetComponentsInChildren<Item>(true); // 아이템들 저장
        for (int i = 0; i < items.Length; i++)
        {
            if (!_objectDic.ContainsKey(items[i].GetType()))
                _objectDic.Add(items[i].GetType(), items[i].gameObject);
        }
        Door[] doors = _map.GetComponentsInChildren<Door>(true); // 문들 저장
        for (int i = 0; i < doors.Length; i++)
        {
            if (!_objectDic.ContainsKey(doors[i].GetType()))
            {
                _objectDic.Add(doors[i].GetType(), doors[i].gameObject);
                doorsL.Add(doors[i].GetType());
            }
        }
        NPC[] npcs = _map.GetComponentsInChildren<NPC>(true); // npc들 저장
        for (int i = 0; i < npcs.Length; i++)
        {
            if (!_objectDic.ContainsKey(npcs[i].GetType()))
                _objectDic.Add(npcs[i].GetType(), npcs[i].gameObject);
        }

        for(int i = 0; i < _objects.Length; i++)
        {
            MonoBehaviour mono = _objects[i].GetComponent<MonoBehaviour>(); // 그 외에 Inspector에서 할당한 오브젝트들 저장
            if (!_objectDic.ContainsKey(mono.GetType()))
                _objectDic.Add(mono.GetType(), _objects[i]);
        }

        for(int i = 0; i < dreamBackGround.childCount; i++)                 //꿈맵안에 있는 맵들 넣기
        {
            Transform child = dreamBackGround.GetChild(i);
            if (child.CompareTag("BackGround") && !_mapDicinString.ContainsKey(child.name))
            {
                _mapDicinString.Add(child.name, child.gameObject);
            }
        }

        for (int i = 0; i < realBackGround.childCount; i++)                 //꿈맵안에 있는 맵들 넣기
        {
            Transform child = realBackGround.GetChild(i);
            if (child.CompareTag("BackGround") && !_mapDicinString.ContainsKey(child.name))      //현실 맵 안에 있는 맵들 넣기
            {
                _mapDicinString.Add(child.name, child.gameObject);
            }
        }

    }

    public static T GetObject<T>() where T : MonoBehaviour
    {
        if (_objectDic.TryGetValue(typeof(T), out GameObject gameObject))
            return gameObject.GetComponent<T>();
        Debug.LogError($"<ObjectManager> 타입이 {typeof(T)}인 오브젝트가 ObjectManager에 등록되어 있지 않습니다.");
        return null;
    }

    public static GameObject GetObject(string _mapName)
    {
        if (_mapDicinString.TryGetValue(_mapName, out GameObject _gameObject))
            return _gameObject;
        else
        {
            Debug.LogError($"{_mapName}을 가진 맵이 없거나 철자가 틀렸습니다.");
            return null;
        }
    }

    public void ApplyDoorStatus(List<string> doorName, List<bool> doorStatus)
    {
        for(int i = 0; i < doorName.Count; i++)
        {
            Type type = Type.GetType(doorName[i]);
            _objectDic[type].GetComponent<Door>().isOpened = doorStatus[i];
        }
    }
}
