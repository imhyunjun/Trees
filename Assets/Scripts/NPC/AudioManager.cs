using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum Category
    {
        BGM,
        Player,
        Environment,
        UI
    }

    private static AudioManager instance;
    public static AudioManager Instance => instance;

    [SerializeField] AudioClip[] bgm;
    [SerializeField] AudioClip[] playerSfx;
    [SerializeField] AudioClip[] environmentSfx;
    [SerializeField] AudioClip[] uiSfx;

    private AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        bgmSource = new GameObject().AddComponent<AudioSource>();
        bgmSource.clip = bgm[0];
        bgmSource.loop = true;
        bgmSource.volume = 1;
        bgmSource.Play();
    }

    public void Play(Category category, string name, float volume = 1f, bool isLoop = false, float delay = 0f)
    {
        AudioClip[] clips = null;
        switch (category)
        {
            case Category.BGM:
                clips = bgm;
                break;

            case Category.Player:
                clips = playerSfx;
                break;

            case Category.Environment:
                clips = environmentSfx;
                break;

            case Category.UI:
                clips = uiSfx;
                break;
        }

        for (int i = 0; i < clips.Length; i++)
        {

            if(clips[i].name == name)
            {
                AudioSource audioSource = category == Category.BGM ? bgmSource : new GameObject().AddComponent<AudioSource>();
                audioSource.clip = clips[i];
                audioSource.volume = volume;
                audioSource.loop = isLoop;
                audioSource.PlayDelayed(delay);
                break;
            }
        }
    }
}
