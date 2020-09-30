using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour             //일단 급하게 추가 나중에 다시 정리
{
    public GameObject QuitPanel;
    bool paused;

    private void Awake()
    {
        paused = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitPanel.SetActive(!paused);
            paused = !paused;
            if (paused) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
    }
    public void OnClickQuitButton()
    {
        Application.Quit(0);
    }
}
