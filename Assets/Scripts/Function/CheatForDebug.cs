using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatForDebug : DontDestroy<CheatForDebug>
{
    [Header("버그를 사용하시겠습니까")]
    [Tooltip("버그사용유무")]
    [SerializeField]
    private bool isUsingCheat;
  
    [SerializeField]
    private ProgressStatus progress;

    [SerializeField]
    [ContextMenuItem("Prologue", "ToPrologue")]
    [ContextMenuItem("DreamMap", "ToDreamMap")]
    private string sceneName;

    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if(isUsingCheat)
        {
            SetCondition();
        }
    }

    void SetCondition()
    { 
         PlayerScan.instance.progressStatus = progress;
    }

    void ToPrologue()
    {
        if (isUsingCheat)
            SceneManager.LoadScene("Prologue");
    }

    void ToDreamMap()
    {
        if (isUsingCheat)
            SceneManager.LoadScene("DreamMap");
    }
}
