using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
<<<<<<< Updated upstream
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
        playerAudio.clip = playerSoundDic[GameManager.Instance.locationPlayerIsIn];
    }

=======
>>>>>>> Stashed changes
    public void Walk()
    {
        switch (GameManager.instance.locationPlayerIsIn)
        {
            case "House":
                SoundManager.PlayCappedSFX("Footstep_inside3_re", "Player");
                break;

            case "DreamMap":
                SoundManager.PlayCappedSFX("Footstep3_dream", "Player");
                break;
        }
    }
}