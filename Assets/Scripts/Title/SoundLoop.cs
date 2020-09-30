using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLoop : MonoBehaviour
{
    void Awake()
    {
        gameObject.GetComponent<AudioSource>().loop = true;
    }
}
