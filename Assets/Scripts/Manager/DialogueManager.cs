using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instanced;
    public static DialogueManager instance => instanced;

    private Dialogue dialogueDB;
    private int dataLength;                             //다이얼로그 데이터의 길이
    //private int dialIndex;                              //다이얼 순번
    //public int currentProcedureIndexE;                  //대화 끝 지점 증가//현재 상황을 알려줌(어떤 아이템을 주웠는지) -> 숫자만큼 다이얼로그에 더해서 나무와 대화 변경
    //public int currentProcedureIndexS;                  //시작점 증가

    Dictionary<string, List<string>> dialogueDic = new Dictionary<string, List<string>>();
    Dictionary<string, List<string>> dialogueNameDic = new Dictionary<string, List<string>>();

    public string[] dialogueString;                     //다이얼로그들 string으로 변환 한 것
    public Text dialText;
    public Text nameText;

    void Awake()
    {
        if (instanced == null)
        {
            instanced = this;
        }
        else if (instanced != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DialoguePanel.instance.Hide();

        dialogueDB = Resources.Load<Dialogue>("DialogueDB");                        //Resources폴더 안에 있는 다이얼로그 DB 다운 받기
        dataLength = dialogueDB.dataArray.Length;
        dialogueString = new string[dataLength];                                    //받아온 갯수만큼 배열 만들기 ( 일단은 배열 ) 나중에 queue나 다른 것도 가능

        for (int i = 0; i < dataLength; i++)
        {
            dialogueString[i] = dialogueDB.dataArray[i].Dialogue.ToString();        //각자 넣어놓기
        }
        

        for (int i = 0; i < dataLength;)
        {
            List<string> dialogueList = new List<string>();
            List<string> dialogueNameList = new List<string>();
            if (i < dataLength - 1)
            {
                do
                {
                    dialogueList.Add(dialogueDB.dataArray[i].Dialogue.ToString());
                    dialogueNameList.Add(dialogueDB.dataArray[i].Talking.ToString());
                    i++;
                }
                while (dialogueDB.dataArray[i].Order.ToString() == dialogueDB.dataArray[i - 1].Order.ToString());

                dialogueDic.Add(dialogueDB.dataArray[i - 1].Order.ToString(), dialogueList);
                dialogueNameDic.Add(dialogueDB.dataArray[i - 1].Order.ToString(), dialogueNameList);
            }

            else if (i == dataLength - 1)
            {
                dialogueDic.Add(dialogueDB.dataArray[i].Order.ToString(), dialogueList);
                dialogueNameDic.Add(dialogueDB.dataArray[i].Order.ToString(), dialogueNameList);
                i++;
            }
        }                                           //dic 초기화
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        DialoguePanel.instance.Show(); //시작 후 1초 뒤 대화창 활성화

        StartCoroutine(PlayText("prologue_0"));

        ButtonPanel.instance.SetUp(() =>
        {
            StartCoroutine(GameManager.instance.IFadeIn(3f));
            StartCoroutine(IContinueDialogue("prologue_2"));                                            //혼잣말 시작
        }, () =>
        {
            StartCoroutine(PlayText("prologue_1"));
        });

    }

    //public IEnumerator IContinueDialogue(int _currentIndex, int _until) //(System이 아닌 일반 대화용) 몇번째 까지 실행
    //{
    //    DialoguePanel.instance.Show();

    //    while (_currentIndex < _until +  + 1)
    //    {
    //        string testText=null;
    //        nameText.text = dialogueDB.dataArray[_currentIndex].Talking;
    //        dialText.text = null;
    //        string dialTxt = dialogueString[_currentIndex].ToString(); // 한 글자씩 나오게 하는 코드
    //        foreach (char c in dialTxt)
    //        {
    //            SoundManager.PlaySFX("Text_typing");
    //            bool stop = false;
    //            if (stop == false)
    //            {
    //                testText += c;
    //                stop = true;
    //            }
    //            dialText.text = testText;
    //            yield return new WaitForSeconds(0.05f);
    //        }
    //        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));                     //엔터키 전까지 기다리기
    //        SoundManager.PlaySFX("Script_3 (1)");
    //        _currentIndex++;
    //        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); //다음 창 바로 뜨지 않게 다시 엔터키 전까지 기다리게 하기
    //        dialText.text = null;
    //    }

    //    DialoguePanel.instance.Hide();
    //}

    //public IEnumerator IContinueDialogue(int _currentIndex, int _until, int _howMuchIncreseStart, int _howMuchIncreseEnd) //(System이 아닌 일반 대화용) 몇번째 까지 실행
    //{
    //    DialoguePanel.instance.Show();

    //    while (_currentIndex + _howMuchIncreseStart < _until + 1 + _howMuchIncreseEnd)
    //    {
    //        string testText = null;
    //        //dialText.text = dialogueString[_currentIndex].ToString();
    //        nameText.text = dialogueDB.dataArray[_currentIndex + _howMuchIncreseStart].Talking;
    //        dialText.text = null;
    //        string dialTxt = dialogueString[_currentIndex + _howMuchIncreseStart].ToString(); // 한 글자씩 나오게 하는 코드
    //        foreach (char c in dialTxt)
    //        {
    //            SoundManager.PlaySFX("Text_typing");
    //            bool stop = false;
    //            if (stop == false)
    //            {
    //                testText += c;
    //                stop = true;
    //            }
    //            dialText.text = testText;
    //            yield return new WaitForSeconds(0.05f);
    //        }
    //        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));                     //엔터키 전까지 기다리기
    //        SoundManager.PlaySFX("Script_3 (1)");
    //        _currentIndex++;
    //        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); //다음 창 바로 뜨지 않게 다시 엔터키 전까지 기다리게 하기
    //        dialText.text = null;
    //    }

    //    DialoguePanel.instance.Hide();
    //}

    public IEnumerator PlayText(string _dialogueOrder)                 //시스템과 대화할 때 나오는 경우 - 왜 시스템만 했을까 그분이
    {
        dialText.text = null;
        nameText.text = dialogueNameDic[_dialogueOrder][0];
        string dialTxt = dialogueDic[_dialogueOrder][0]; // 한 글자씩 나오게 하는 코드
        foreach (char c in dialTxt)
        {
            SoundManager.PlaySFX("Text_typing");
            dialText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    public IEnumerator IContinueDialogue(string _dialogueOrder) 
    {
        DialoguePanel.instance.Show();
        int i = 0;
        while (i < dialogueDic[_dialogueOrder].Count)
        {
            string testText = null;
            nameText.text = dialogueNameDic[_dialogueOrder][i];
            dialText.text = null;
            string dialTxt = dialogueDic[_dialogueOrder][i];
            foreach (char c in dialTxt)
            {
                SoundManager.PlaySFX("Text_typing");
                bool stop = false;
                if (stop == false)
                {
                    testText += c;
                    stop = true;
                }
                dialText.text = testText;
                yield return new WaitForSeconds(0.05f);
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return));                     //엔터키 전까지 기다리기
            SoundManager.PlaySFX("Script_3 (1)");
            i++;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); //다음 창 바로 뜨지 않게 다시 엔터키 전까지 기다리게 하기
            dialText.text = null;
        }

        DialoguePanel.instance.Hide();
    }

}
