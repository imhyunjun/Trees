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
    Dictionary<string, Sprite> dialogueBalloonDic = new Dictionary<string, Sprite>();

    [SerializeField]
    private GameObject dialogueBalloon;
    [SerializeField]
    private RectTransform cavasRect;
    [SerializeField]
    private Sprite[] ballonSprites;

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

        for(int i = 0; i < ballonSprites.Length; i++) // 말풍선 그림들 딕셔녀리에 추가
        {
            dialogueBalloonDic.Add(ballonSprites[i].name, ballonSprites[i]);
        }
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);

        PlayDialogue("prologue_0", true);

        ButtonPanel.instance.SetUp(() =>
        {
            StartCoroutine(GameManager.instance.IFadeIn(3f));
            PlayDialogue("prologue_2");                                 //혼잣말 시작
        }, () =>
        {
            PlayDialogue("prologue_1", true);
        });
    }


    private Coroutine _playDialogueCor = null;
    public Coroutine playDialogueCor => _playDialogueCor;
    private bool skip = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && _playDialogueCor != null)
            skip = true;
    }


    public void PlayDialogue(string _dialogueOrder, bool _isQuesstion = false, System.Action _onComplete = null)  // isQuestion이 true이면 마지막에 엔터를 쳐도 대화창이 사라지지 않음
    {
        if (_playDialogueCor != null)
            StopCoroutine(_playDialogueCor);
        if (_isQuesstion)
            _playDialogueCor = StartCoroutine(IQuestionDialogue(_dialogueOrder));
        else
            _playDialogueCor = StartCoroutine(IContinueDialogue(_dialogueOrder, _onComplete));
    }

    private IEnumerator IQuestionDialogue(string _dialogueOrder)                // 시스템이 질문할 떄 사용
    {
        DialoguePanel.instance.Show(0);
        dialText.text = null;
        nameText.text = dialogueNameDic[_dialogueOrder][0];
        string dialTxt = dialogueDic[_dialogueOrder][0]; 
        skip = false;
        foreach (char c in dialTxt)
        {
            if (skip)
            {
                dialText.text = dialTxt;
                break;
            }
            SoundManager.PlaySFX("Text_typing");
            dialText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        _playDialogueCor = null;
    }

    private IEnumerator IContinueDialogue(string _dialogueOrder, System.Action _OnComplete)  // 대화
    {
        DialoguePanel.instance.Show(0);
        int i = 0;
        while (i < dialogueDic[_dialogueOrder].Count)
        {
            skip = false;
            string testText = null;
            nameText.text = dialogueNameDic[_dialogueOrder][i];
            dialText.text = null;
            string dialTxt = dialogueDic[_dialogueOrder][i];
            foreach (char c in dialTxt)
            {
                if (skip)
                {
                    dialText.text = dialTxt;
                    break;
                }
                SoundManager.PlaySFX("Text_typing");
                testText += c;
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
        _OnComplete?.Invoke();
        _playDialogueCor = null;
    }

    // Key: 말하는 사람 오브젝트, Value : dialogue order
    public void ShowDialogueBallon(List<KeyValuePair<GameObject, string>> _dialogueList, float _scale = 1f, float _y = 5.5f) // 원희님이 사진으로 주셔서 일단 이렇게 해놨어요ㅜㅜㅜ
    {                                                                                                                                       // .더 좋은 구조를 계속 고민해봐야 할것 같아요ㅜㅜ
        StartCoroutine(IShowDialogueBallonSprite(_dialogueList, _scale, _y));
    }

    private IEnumerator IShowDialogueBallonSprite(List<KeyValuePair<GameObject, string>> _dialogueList, float _scale, float _y)
    {
        for(int i = 0; i < _dialogueList.Count; i++)
        {
            KeyValuePair<GameObject, string> pair = _dialogueList[i];
            GameObject gameObject = new GameObject("ballon");
            gameObject.transform.localScale = Vector3.one * _scale;
            gameObject.transform.SetParent(pair.Key.transform);
            gameObject.transform.localPosition = new Vector3(0, _y, 0);
            SpriteRenderer sr = gameObject.AddComponent<SpriteRenderer>();
            sr.sortingOrder = 10;
            if (dialogueBalloonDic.TryGetValue(pair.Value, out Sprite sprite))
                sr.sprite = sprite;
            else
                Debug.Log($"이름이 {pair.Value}인 스프라이트가 없습니다.");

            yield return new WaitForSeconds(2f); // 말풍선이 지속되는 시간

            Destroy(gameObject);

            yield return new WaitForSeconds(1f); // 다음 말풍선과의 사이 텀
        }
    }

    /// <summary>
    /// 말풍선 띄우기
    /// </summary>
    /// <param name="_whoIsTalking"></param>    위치를 말하는 사람 기준으로 정하기
    public IEnumerator IShowDialogueBalloon(GameObject _whoIsTalking, string _dialogueOrder)
    {
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(_whoIsTalking.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * cavasRect.sizeDelta.x) - (cavasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * cavasRect.sizeDelta.y) - (cavasRect.sizeDelta.y * 0.5f)));
        dialogueBalloon.GetComponent<RectTransform>().anchoredPosition = WorldObject_ScreenPosition + new Vector2(0f, 100f);
        Debug.Log(dialogueBalloon.GetComponent<RectTransform>().anchoredPosition);
        Debug.Log(WorldObject_ScreenPosition);

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
