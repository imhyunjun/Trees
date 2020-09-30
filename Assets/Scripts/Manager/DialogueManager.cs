using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    Dialogue dialogueDB;
    int dataLength;                             //다이얼로그 데이터의 길이
    int dialIndex;                              //다이얼 순번
    public int currentProcedureIndexE;          //대화 끝 지점 증가//현재 상황을 알려줌(어떤 아이템을 주웠는지) -> 숫자만큼 다이얼로그에 더해서 나무와 대화 변경
    public int currentProcedureIndexS;          //시작점 증가

    public string[] dialogueString;             //다이얼로그들 string으로 변환 한 것
    public bool isDialogueActive;               //다이얼로그가 활성화 되있는지
    public GameObject dialogueImage;            //대화 이미지
    public Text dialText;
    public Text nameText;

    public GameObject buttonPanel;
    public GameObject yButton;                  //yes 버튼
    private ButtonClick yButtonClick;

    public AudioClip scriptAudioClip;           //오디오 소스!
    AudioSource scriptAudioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        currentProcedureIndexE = 0;
        currentProcedureIndexS = 0;

        DontDestroyOnLoad(gameObject);
        isDialogueActive = false;

        dialogueDB = Resources.Load<Dialogue>("DialogueDB");                        //Resources폴더 안에 있는 다이얼로그 DB 다운 받기
        dataLength = dialogueDB.dataArray.Length;
        dialogueString = new string[dataLength];                                    //받아온 갯수만큼 배열 만들기 ( 일단은 배열 ) 나중에 queue나 다른 것도 가능

        for (int i = 0; i < dataLength; i++)
        {
            dialogueString[i] = dialogueDB.dataArray[i].Dialogue.ToString();        //각자 넣어놓기
        }

        scriptAudioSource = gameObject.GetComponent<AudioSource>();
        scriptAudioSource.clip = scriptAudioClip;
    }

    IEnumerator Start()
    {
        dialIndex = 1;
        yButtonClick = yButton.gameObject.GetComponent<ButtonClick>();                              //초기화

        yield return new WaitForSeconds(1f);
        OnOffDialogueImage(true);                                                                   //시작 후 1초 뒤 대화창 활성화
        buttonPanel.SetActive(true);                                                                //버튼 창도 활성화

        StartCoroutine(PlayText(dialIndex));
       
        yield return new WaitUntil(() => ButtonPanel.yButtonisClicked);                            //y버튼이 눌릴때까지대기
        yield return StartCoroutine(GameManager.instance.IFadeIn(3f));                              //페이드 아웃이 끝난 후
        yield return new WaitForSeconds(1f);

        dialIndex = 3;                                                                              //3번 다이얼로그부터 시작
        StartCoroutine(IContinueDialogue(dialIndex, 4));                                            //혼잣말 시작
    }

    private void Update()
    {
        dialogueImage.SetActive(isDialogueActive);
    }

    public bool GetDialogueActive()
    {
        return isDialogueActive;
    }

    public IEnumerator IContinueDialogue(int _currentIndex, int _until) //(System이 아닌 일반 대화용) 몇번째 까지 실행
    {
        OnOffDialogueImage(true);

        while (_currentIndex < _until +  + 1)
        {
            string testText=null;
            nameText.text = dialogueDB.dataArray[_currentIndex].Talking;
            dialText.text = null;
            string dialTxt = dialogueString[_currentIndex].ToString(); // 한 글자씩 나오게 하는 코드
            foreach (char c in dialTxt)
            {
                dialText.GetComponent<AudioSource>().Play();
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
            scriptAudioSource.Play();
            _currentIndex++;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); //다음 창 바로 뜨지 않게 다시 엔터키 전까지 기다리게 하기
            dialText.text = null;
        }
        OnOffDialogueImage(false);                                                                  //대화 끝나면 해제
    }

    public IEnumerator IContinueDialogue(int _currentIndex, int _until, int _howMuchIncreseStart, int _howMuchIncreseEnd) //(System이 아닌 일반 대화용) 몇번째 까지 실행
    {
        OnOffDialogueImage(true);

        while (_currentIndex + _howMuchIncreseStart < _until + 1 + _howMuchIncreseEnd)
        {
            string testText = null;
            //dialText.text = dialogueString[_currentIndex].ToString();
            nameText.text = dialogueDB.dataArray[_currentIndex + _howMuchIncreseStart].Talking;
            dialText.text = null;
            string dialTxt = dialogueString[_currentIndex + _howMuchIncreseStart].ToString(); // 한 글자씩 나오게 하는 코드
            foreach (char c in dialTxt)
            {
                dialText.GetComponent<AudioSource>().Play();
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
            scriptAudioSource.Play();
            _currentIndex++;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); //다음 창 바로 뜨지 않게 다시 엔터키 전까지 기다리게 하기
            dialText.text = null;
        }
        OnOffDialogueImage(false);                                                                  //대화 끝나면 해제
    }

    public void OnOffDialogueImage(bool _active)
    {
        isDialogueActive = _active;
    }

    public IEnumerator PlayText(int _index)                 //시스템과 대화할 때 나오는 경우 - 왜 시스템만 했을까 그분이
    {
        dialText.text = null;

        nameText.text = dialogueDB.dataArray[_index].Talking;
        string dialTxt = dialogueString[_index].ToString(); // 한 글자씩 나오게 하는 코드
        foreach (char c in dialTxt)
        {
            dialText.GetComponent<AudioSource>().Play();
            dialText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
    }


}
