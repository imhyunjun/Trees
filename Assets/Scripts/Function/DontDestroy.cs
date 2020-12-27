using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy<T> : MonoBehaviour where T : DontDestroy<T>
{
    private static T _instance;
    public static T instance => _instance;
    protected void Awake()
    {
        if (_instance == null)
            _instance = this as T;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
