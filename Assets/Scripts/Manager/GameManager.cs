using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum ProgressStatus
{
    E_Start,
    E_ChangeClothes,
    E_EatMedicine,
    E_Sleep,
    E_TalkWithTreeFirstTime,
    E_TalkWithPastMom,
    E_TalkWithPastJung,
    E_TalkWithPastDad,
    E_GetBackMirror,
    E_GiveBackMirrorToTree,
    E_TalkWithCurrentDad,
    E_GetCashNCard,
    E_GetAlcholBottle,
    E_PayedDone,
    E_ErrandFinished,
    E_JungWannaKillFather
}

public delegate void SceneEventHandler(bool _changeScene);

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objects;

    private static GameManager instanced;
    public static GameManager instance => instanced;

    public int treeGrowStatus;
    public GameObject fadeObject;                       //페이드효과 줄 것
    public GameObject player;                           //플레이어오브젝트 - 씬 로드할때 
    public string locationPlayerIsIn;                   //플레이어가 있느 장소 - 발자국 사운드 관리

    private Image fadeImg;                                      //페이드 효과에 쓸 화면 색깔
    private Color tempColor;                                    //색 바꿀때 쓸 임시 색

    private static Dictionary<Type, GameObject> objectTypeDic;
    private Dictionary<string, GameObject> objectNameDic;


    private void Awake()
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

        treeGrowStatus = 0;
        locationPlayerIsIn = "House";

        fadeImg = fadeObject.GetComponent<Image>();
        tempColor = fadeImg.color;
        tempColor.a = 1;
        fadeImg.color = tempColor;                      //시작시 이미지 검은 화면

        InitializeObejctDic();
    }

    private void InitializeObejctDic()
    {
        objectTypeDic = new Dictionary<Type, GameObject>();
        objectNameDic = new Dictionary<string, GameObject>();
        for(int i = 0; i < objects.Length; i++)
        {
            MonoBehaviour mono = objects[i].GetComponent<MonoBehaviour>();
            if (mono != null) // 별도의 클래스가 있다면 Type 딕셔너리에, 나중에 클래스로 검색
                objectTypeDic.Add(mono.GetType(), objects[i]);
            else
                objectNameDic.Add(objects[i].gameObject.name, objects[i].gameObject); // 별도의 클래스가 없다면 Name 딕셔너리에, 나중에 이름으로 검색
        }
    }

    public GameObject GetObject(string name) // 이름으로 검색  ex) 슈퍼마켓 아줌마는 따로 클래스가 없어서 이거 사용
    {
        if (objectNameDic.TryGetValue(name, out GameObject target))
            return target;
        Debug.LogError($"GameObject의 이름이 {name}인 오브젝트가 없습니다.");
        return null;
    }
    public static T GetObject<T>() where T : MonoBehaviour // 클래스로 검색
    {
        if (objectTypeDic.TryGetValue(typeof(T), out GameObject target))
            return (T)target.GetComponent<MonoBehaviour>();
        Debug.LogError($"타입이 {typeof(T)}인 오브젝트가 없습니다.");
        return default(T);
    }


    //나중에 fade in/ fadeout은 많이 쓸 것 같아서 일단 만듦
    public IEnumerator IFadeIn(float _fadeinTime)               //이건 그냥 지
    {
        Color color = fadeImg.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / _fadeinTime;
            fadeImg.color = color;
            yield return null;
        }
        if (color.a <= 0f) color.a = 0f;
        fadeImg.color = color;
    }

    public IEnumerator IFadeOut(float _fadeOutTime) //fade out
    {
        Color color = fadeImg.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / _fadeOutTime;
            fadeImg.color = color;
            yield return null;
        }
        if (color.a >= 1f) color.a = 1f;
        fadeImg.color = color;
    }

    public IEnumerator IFadeIn(float _fadeinTime, GameObject _gameObject) //fade in
    {
        Image img = _gameObject.GetComponent<Image>();
        Color color = img.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / _fadeinTime;
            img.color = color;
            yield return null;
        }
        if (color.a <= 0f) color.a = 0f;
        img.color = color;
    }

    public IEnumerator IFadeOut(float _fadeOutTime, GameObject _gameObject) //fade out
    {
        Image img = _gameObject.GetComponent<Image>();
        Color color = img.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / _fadeOutTime;
            img.color = color;
            yield return null;
        }
        if (color.a >= 1f) color.a = 1f;
        img.color = color;
    }

    public  void StartLoadSceneCor(string _sceneName, float _fadeOutTime, float _fadeInTime, string _playerIn, System.Action callBack = null)
    {
        StartCoroutine(ILoadScene(_sceneName, _fadeOutTime, _fadeInTime, _playerIn, callBack));
    }

    public IEnumerator ILoadScene(string _sceneName, float _fadeOutTime, float _fadeInTime, string _playerIn, System.Action callBack = null)  
    {
        yield return StartCoroutine(IFadeOut(_fadeOutTime));                      // 완전히 페이트 아웃 할 때 까지 대기

        SceneManager.LoadScene(_sceneName);

        Camera.main.transform.position = new Vector3(0, 0, -10);                    //카메라 좌표 설정
        player.transform.position = new Vector2(0, -3);                             //씬 이동 후 좌표 설정
        player.GetComponent<SpriteRenderer>().sortingOrder = 3;
        locationPlayerIsIn = _playerIn;

        StartCoroutine(IFadeIn(_fadeInTime));

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == _sceneName);    // 씬 로드될 때 까지 대기

        callBack?.Invoke();
    }
}