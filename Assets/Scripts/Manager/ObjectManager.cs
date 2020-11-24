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
    public static ObjectManager instance => _instance;

    private void Awake()
    {
        if (_instance == null)
            _instance = this;

        _objectDic = new Dictionary<Type, GameObject>(); // 클래스별로 딕셔너리에 저장

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
                _objectDic.Add(doors[i].GetType(), doors[i].gameObject);
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
    }

    public static T GetObject<T>() where T : MonoBehaviour
    {
        if (_objectDic.TryGetValue(typeof(T), out GameObject gameObject))
            return gameObject.GetComponent<T>();
        Debug.LogError($"<ObjectManager> 타입이 {typeof(T)}인 오브젝트가 ObjectManager에 등록되어 있지 않습니다.");
        return null;
    }
}
