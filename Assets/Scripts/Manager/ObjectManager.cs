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

    private static ObjectManager _instance;
    private static Dictionary<Type, GameObject> _objectDic;

    //스크립트 없는 배경들 가져오기 위해, 맵마다 스크립트 만들기 귀찮아서 일단 이렇게 했어요
    private static Dictionary<string, GameObject> _objectDicinString;
    
    public static ObjectManager instance => _instance;

    public List<string> doorsL = new List<string>();

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        _objectDic = new Dictionary<Type, GameObject>(); // 클래스별로 딕셔너리에 저장
        _objectDicinString = new Dictionary<string, GameObject>();

        Transform dreamBackGround = _map.transform.GetChild(0).GetChild(0);
        Transform realBackGround = _map.transform.GetChild(1).GetChild(0);

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
                doorsL.Add(doors[i].GetType().ToString());
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
            if (child.CompareTag("BackGround") && !_objectDicinString.ContainsKey(child.name))
                _objectDicinString.Add(child.name, child.gameObject);     
        }

        for (int i = 0; i < realBackGround.childCount; i++)                 //꿈맵안에 있는 맵들 넣기
        {
            Transform child = realBackGround.GetChild(i);
            if (child.CompareTag("BackGround") && !_objectDicinString.ContainsKey(child.name))      //현실 맵 안에 있는 맵들 넣기
                _objectDicinString.Add(child.name, child.gameObject);
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
        if (_objectDicinString.TryGetValue(_mapName, out GameObject _gameObject))
            return _gameObject;
        else
        {
            Debug.LogError($"{_mapName}을 가진 맵이 없거나 철자가 틀렸습니다.");
            return null;
        }
    }
}
