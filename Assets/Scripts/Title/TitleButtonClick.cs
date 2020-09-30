using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleButtonClick : MonoBehaviour
{
    public GameObject fadeImg;
    public void OnClickStartButton()
    {
        gameObject.GetComponent<AudioSource>().Play();
        Invoke("startGame", 1.5f); //1.5초 후에 프롤로그 로딩
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("Prologue");
    }

}
