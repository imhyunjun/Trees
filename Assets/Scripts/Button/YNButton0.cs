using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YNButton0 : ButtonPanel, IButtonCommand
{
    public void YButtonExecute()
    {
        StartCoroutine(GameManager.Instance.IFadeIn(3f));                   //3초동안 페이드 인
        yButtonisClicked = true;                                            //y 버튼 눌림
    }

    public void NButtonExecute()
    {
        StartCoroutine(DialogueManager.Instance.PlayText(2));
    }
}
