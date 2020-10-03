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

    //panel그 자체보다 자식 오브젝트 키고 끄기( 대화창, 대화말풍선같은 경우.. ) 
    public virtual void Show(int i)
    {
        gameObject.transform.GetChild(i).gameObject.SetActive(true);
    }
    public virtual void Hide(int i)
    {
        gameObject.transform.GetChild(i).gameObject.SetActive(false);
    }


}
