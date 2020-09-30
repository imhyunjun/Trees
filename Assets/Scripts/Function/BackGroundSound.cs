using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundSound : MonoBehaviour
{
    public AudioClip livingRoom;
    public AudioClip dreamMap;

    AudioSource backGroundSound;

    Dictionary<string, AudioClip> backGroundSoundList = new Dictionary<string, AudioClip>();    //맵 이름으로 할지 씬이름으로 할지 나중에 정하기

    public void Awake()
    {
        backGroundSound = gameObject.GetComponent<AudioSource>();
        backGroundSoundList.Add("Prologue", livingRoom);            
        backGroundSoundList.Add("DreamMap", dreamMap);
        backGroundSoundList.Add("Chapter1", livingRoom);                //사운드는 나중에
    }

    public void Update()
    {
        backGroundSound.clip = backGroundSoundList[SceneManager.GetActiveScene().name];
        if(!backGroundSound.isPlaying)
        {
            backGroundSound.Play();
        }
        gameObject.GetComponent<AudioSource>().loop = true;
    }
}
