using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleButtonClick : MonoBehaviour
{
    [SerializeField]
    private Button startButton;

    public void OnClickStartButton()
    {
        startButton.interactable = false;
        SoundManager.PlaySFX("Click_3");
        Invoke("startGame", 1.5f); //1.5초 후에 프롤로그 로딩
    }

    public void startGame()
    {
        SceneManager.LoadScene("Prologue");
        startButton.interactable = true;
    }
}