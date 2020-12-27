using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum BGM
{
    Title,
    LivingRoom,
    JungRoom,
    DreamMap
}

public class BGMManager : DontDestroy<BGMManager>
{
    [SerializeField]
    private AudioClip[] bgms;

    private void Start()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Title":
                PlayBGM(BGM.Title);
                break;

            case "Prologue":
                PlayBGM(BGM.LivingRoom);
                break;
        }
    }

    public void PlayBGM(BGM bgm)
    {
        SoundManager.Play(bgms[(int)bgm], true);
    }
}
