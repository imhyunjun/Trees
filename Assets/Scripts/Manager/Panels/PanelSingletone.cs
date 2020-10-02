using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSingletone<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                T[] objectList = Resources.FindObjectsOfTypeAll<T>();
                if (objectList.Length > 0)
                    (objectList[0] as PanelSingletone<T>).Awake();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this as T;
    }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

}
