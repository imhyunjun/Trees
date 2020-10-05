using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Vector3 dd;
    public GameObject d;

    private void Awake()
    {
        d.transform.position = Vector3.zero;
    }

    private void Update()
    {
        d.transform.position = dd;
    }
}
