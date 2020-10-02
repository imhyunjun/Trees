using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy<T> : MonoBehaviour where T : DontDestroy<T>
{
    private static T instance;
    private void Awake()
    {
        if (instance == null)
            instance = this as T;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
}
