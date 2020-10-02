using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum ProgressStatus
{
    E_Start,
    E_ChangeClothes,
    E_EatMedicine,
    E_Sleep,
    E_TalkWithPastMom,
    E_TalkWithPastJung,
    E_TalkWithPastDad,
    E_GetBackMirror,
    E_GiveBackMirrorToTree
}

public delegate void SceneEventHandler(bool _changeScene);

public class GameManager : MonoBehaviour
{
    private static GameManager instanced;
    public static GameManager instance => instanced;

    public int treeGrowStatus;
    public GameObject fadeObject;                       //페이드효과 줄 것
    public GameObject player;                           //플레이어오브젝트 - 씬 로드할때 
    public string locationPlayerIsIn;                   //플레이어가 있느 장소 - 발자국 사운드 관리

    private Image fadeImg;                                      //페이드 효과에 쓸 화면 색깔
    private Color tempColor;                                    //색 바꿀때 쓸 임시 색

    public event SceneEventHandler ChangeSceneEvent;

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
        if (color.a >= 1f) color.a = 0f;
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
        if (color.a >= 1f) color.a = 0f;
        img.color = color;
    }

    public IEnumerator ILoadScene(string _sceneName, float _fadetime, string _playerIn, System.Action callBack = null)     //일단 변수 3개 마지막 변수는 이동방법 및, 씬 이름나오면 나중에
    {
        yield return StartCoroutine(IFadeOut(_fadetime));
        player.transform.position = new Vector2(0, -3);                             //씬 이동 후 좌표 설정
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(_sceneName);

        Camera.main.transform.position = new Vector3(0, 0, -10);                    //카메라 좌표 설정
        player.transform.position = new Vector2(0, -3);                             //씬 이동 후 좌표 설정
        player.GetComponent<SpriteRenderer>().sortingOrder = 3;

        locationPlayerIsIn = _playerIn;
        callBack?.Invoke();
    }
}

