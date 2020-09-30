using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPastFamily : MonoBehaviour
{
    //public int startDialogueIndex;      //각자 엄마, 아빠 ,정이 가지고 있는 숫자로 했었는데 잘 안되서 그냥 아래처럼 
    //public int endDialogueIndex;
    bool isClickedJung = false;
    bool isClickedMoM = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero, Mathf.Infinity);
            if(isClickedJung == true && hit.collider != null && hit.collider.name == "PastFather")
            {
                StartCoroutine(DialogueManager.instance.IContinueDialogue(31, 33));
                StartCoroutine(SettingActive());
            }

            if ( hit.collider != null && hit.collider.name == "PastMother")
            {
                StartCoroutine(DialogueManager.instance.IContinueDialogue(25, 27));
                isClickedMoM = true;
            }

            if (isClickedMoM == true && hit.collider != null && hit.collider.name == "PastJung")
            {
                StartCoroutine(DialogueManager.instance.IContinueDialogue(28, 30));
                isClickedJung = true;
            }
        }
    }

    IEnumerator SettingActive()
    {
        bool stop = false;
        while (stop == false)
        {
            yield return StartCoroutine(DialogueManager.instance.IContinueDialogue(31, 33));                    //아빠 대화 끝나고 
            yield return new WaitForSeconds(1.5f);                                                              //1.5초 후에
            gameObject.GetComponent<AudioSource>().Play();                                                      //사이렌 소리~~ 시작 언제 끝나지
            GameObject.Find("PastAccidentRoom").transform.Find("BrokenBackMirror").gameObject.SetActive(true);  //등장
            yield return new WaitForSeconds(1.5f);                                                              //임시로 끝나는거 설정
            gameObject.GetComponent<AudioSource>().Stop();
            stop = true;
        }
    }
}

