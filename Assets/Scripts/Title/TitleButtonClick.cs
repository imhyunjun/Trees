using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButtonClick : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button loadButton;
    [SerializeField]
    private DataManager dataManager;

    public void OnClickStartButton()
    {
        startButton.interactable = false;
        loadButton.interactable = false;
        SoundManager.PlaySFX("Click_3");
        Invoke("startGame", 1.5f); //1.5초 후에 프롤로그 로딩
    }

    public void OnClickLoadLastGameButton()
    {
        startButton.interactable = false;
        loadButton.interactable = false;
        SoundManager.PlaySFX("Click_3");
    }

    public void startGame()
    {
        SceneManager.LoadScene("Prologue");
        startButton.interactable = true;
        BGMManager.instance.PlayBGM(BGM.LivingRoom);
    }
}