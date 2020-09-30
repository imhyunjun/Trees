using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClick : ButtonPanel
{
    
    public static int dialIndex = 2;

    Queue<IButtonCommand> buttonClickMethods = new Queue<IButtonCommand>();             //y, n버튼 기능 모음
    YNButton0 ynButton0;
    YNButton1 ynButton1;

    private void Start()
    {
        ynButton0 = gameObject.AddComponent<YNButton0>() as YNButton0;
        ynButton1 = gameObject.AddComponent<YNButton1>() as YNButton1;

        buttonClickMethods.Enqueue(ynButton0);
        buttonClickMethods.Enqueue(ynButton1);
    }

    public void OnClickYesButton()                                      //yes버튼이 눌리면
    {
        gameObject.GetComponent<AudioSource>().Play();

        DialogueManager.instance.dialText.text = "";                    //대화창 초기화
        DialogueManager.instance.isDialogueActive = false;
        OnOffTheChildButton(0, false);
        OnOffTheChildButton(1, false);                                  //0,1번 버튼 끄기

        buttonClickMethods.Dequeue().YButtonExecute();                  //큐의 제일 앞부분의 y버튼 기능 사용 후 제거
    }

    public void OnClickNoButton()                                       //no버튼 클릭시 다이얼로그 1번 출력
    {
        gameObject.GetComponent<AudioSource>().Play();
        DialogueManager.instance.dialText.text = null;
        buttonClickMethods.Peek().NButtonExecute();                     //큐의 제일 앞부분의 n버튼 기능 사용
    }
}
