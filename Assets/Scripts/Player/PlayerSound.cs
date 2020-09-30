using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public AudioClip walkInHouse;
    public AudioClip walkInDreamMap;

    Dictionary<string, AudioClip> playerSoundDic = new Dictionary<string, AudioClip>();

    AudioSource playerAudio;

    private void Awake()
    {
        playerAudio = gameObject.GetComponent<AudioSource>();

        playerSoundDic.Add("House", walkInHouse);
        playerSoundDic.Add("DreamMap", walkInDreamMap);
        playerSoundDic.Add("NoWhere", null);
    }

    private void Update()
    {
        playerAudio.clip = playerSoundDic[GameManager.instance.locationPlayerIsIn];
    }

    public void Walk()
    {
        if(playerAudio.clip != null)
            playerAudio.Play();   
    }
}
