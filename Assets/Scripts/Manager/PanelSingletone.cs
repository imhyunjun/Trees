using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSingletone<T> : MonoBehaviour where T : class, new()
{
    protected static T instanced;
   
    public static T instance
    {
        get
        {
            if (instanced == null)
            {
                instanced = new T();
            }

            return instanced;
        }
    }

}
