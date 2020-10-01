using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YNButton1 : ButtonPanel, IButtonCommand
{
    AudioSource waterSwallowing;

    private void Awake()
    {
        waterSwallowing = GameObject.Find("water-gulp").GetComponent<AudioSource>();
    }
    public void YButtonExecute()
    {
        yButtonisClicked = true;                                            //y 버튼 눌림
        PlayerScan.Instance.eatMed = true;
        waterSwallowing.Play();
    }

    public void NButtonExecute()
    {
        StartCoroutine(DialogueManager.Instance.PlayText(10));
    }
}
