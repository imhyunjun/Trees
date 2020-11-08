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
   
    Dictionary<string, List<string>> dialogueDic = new Dictionary<string, List<string>>();
    Dictionary<string, List<string>> dialogueNameDic = new Dictionary<string, List<string>>();

    [SerializeField]
    private GameObject dialogueBalloon;

    public Text dialText;
    public Text nameText;

    public float x;
    public float y;             //말풍선 위치 조절용 임시용

    void Awake()
    {
        if (instanced == null)
        {
            instanced = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        DialoguePanel.instance.Hide(0);
        DialoguePanel.instance.Hide(1);

        dialogueDB = Resources.Load<Dialogue>("DialogueDB");                        //Resources폴더 안에 있는 다이얼로그 DB 다운 받기
        dataLength = dialogueDB.dataArray.Length;
   
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
                dialogueList.Add(dialogueDB.dataArray[i].Dialogue.ToString());
                dialogueNameList.Add(dialogueDB.dataArray[i].Talking.ToString());

                dialogueDic.Add(dialogueDB.dataArray[i].Order.ToString(), dialogueList);
                dialogueNameDic.Add(dialogueDB.dataArray[i].Order.ToString(), dialogueNameList);
                i++;
            }
        }                                           //dic 초기화


        
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        PlayDialogue("prologue_0");

        ButtonPanel.instance.SetUp(() =>
        {
            StartCoroutine(GameManager.instance.IFadeIn(3f));
            PlayDialogue("prologue_2");                                 //혼잣말 시작
        }, () =>
        {
            PlayDialogue("prologue_1");
        });
    }

    //public IEnumerator PlayText(string _dialogueOrder)                 // 대화를 한번만 할 때
    //{
    //    DialoguePanel.instance.Show(0);
    //    dialText.text = null;
    //    nameText.text = dialogueNameDic[_dialogueOrder][0];
    //    string dialTxt = dialogueDic[_dialogueOrder][0]; // 한 글자씩 나오게 하는 코드
    //    foreach (char c in dialTxt)
    //    {
    //        SoundManager.PlaySFX("Text_typing");
    //        dialText.text += c;
    //        yield return new WaitForSeconds(0.05f);
    //    }

    //}

    private Coroutine _playDialogueCor = null;
    public Coroutine playDialogueCor => _playDialogueCor;
    public void PlayDialogue(string _dialogueOrder) 
    {
        if (_playDialogueCor != null)
            StopCoroutine(_playDialogueCor);
        _playDialogueCor = StartCoroutine(IContinueDialogue(_dialogueOrder));
    }

    private IEnumerator IContinueDialogue(string _dialogueOrder) 
    {
        DialoguePanel.instance.Show(0);
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
        DialoguePanel.instance.Hide(0);
        _playDialogueCor = null;
    }

    /// <summary>
    /// 말풍선 띄우기
    /// </summary>
    /// <param name="_whoIsTalking"></param>    위치를 말하는 사람 기준으로 정하기
    public IEnumerator IShowDialogueBalloon(GameObject _whoIsTalking, string _dialogueOrder)
    {
        //dialogueBalloon.transform.position = _whoIsTalking.transform.position + new Vector3(x,y, 0);    //위치 조절 다른 방법..
        //dialogueBalloon.transform.position = _whoIsTalking.transform.position + new Vector3(10f, 10f, 0); -> 이게 왜 안되냐
        //dialogueBalloon.transform.localPosition = _whoIsTalking.transform.position + new Vector3(10f, 10f, 0);
        //dialogueBalloon.transform.position = Vector3.zero; ->원점하면 왜 원점아닌데가 원점이지 
        //dialogueBalloon.GetComponent<RectTransform>().localPosition = _whoIsTalking.transform.position + new Vector3(10f, 10f, 0);
        //Debug.Log(_whoIsTalking.transform.position);
        //Debug.Log(dialogueBalloon.transform.position);
        //Debug.Log(_whoIsTalking.transform.position + new Vector3(10f, 10f, 0));
        //Debug.Log(dialogueBalloon.transform.localPosition);
        //Debug.Log(dialogueBalloon.GetComponent<RectTransform>().position);
        //Debug.Log(dialogueBalloon.GetComponent<RectTransform>().localPosition);

        DialoguePanel.instance.Show(1);
        for (int i = 0; i < dialogueDic[_dialogueOrder].Count; i++)
        {
            dialogueBalloon.transform.GetChild(0).GetComponent<Text>().text = dialogueDic[_dialogueOrder][i];//말풍선은 대화 하나로 가정 ㅠㅠ 두개가 생겼다..
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(3f);
        DialoguePanel.instance.Hide(1);

    }

}
