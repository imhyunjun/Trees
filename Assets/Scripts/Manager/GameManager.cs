using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instanced;
    public static GameManager instance => instanced;

    public int treeGrowStatus;
    public GameObject fadeObject;                       //페이드효과 줄 것
    public GameObject player;                           //플레이어오브젝트 - 씬 로드할

    [SerializeField]
    private GameObject Reality;                         //현실로 돌아올 떄 ( 정이 방)
    [SerializeField]
    private GameObject Dream;                           //꿈으로 갈 떄 ( 나무 방 )
    [SerializeField]
    private GameObject ClassRoom;

    public string locationPlayerIsIn;                   //플레이어가 있느 장소 - 발자국 사운드 관리
    public string map;

    private Image fadeImg;                                      //페이드 효과에 쓸 화면 색깔
    private Color tempColor;                                    //색 바꿀때 쓸 임시 색


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

        fadeImg = fadeObject.GetComponent<Image>();
        tempColor = fadeImg.color;
        tempColor.a = 1;
        fadeImg.color = tempColor;                      //시작시 이미지 검은 화면
    }

    public void GameStart() // 게임 처음부터 시작
    {
        treeGrowStatus = 0;
        locationPlayerIsIn = "LivingRoom";
        map = "LivingRoom";
        StartCoroutine(DialogueManager.instance.GameStart());
    }

    //나중에 fade in/ fadeout은 많이 쓸 것 같아서 일단 만듦
    public IEnumerator IFadeIn(float _fadeinTime)               //이건 그냥 지
    {
        fadeImg.gameObject.SetActive(true);
        Color color = fadeImg.color;
        while (color.a > 0f)
        {
            color.a -= Time.deltaTime / _fadeinTime;
            fadeImg.color = color;
            yield return null;
        }
        if (color.a <= 0f) color.a = 0f;
        fadeImg.color = color;
        fadeImg.gameObject.SetActive(false);
    }

    public IEnumerator IFadeOut(float _fadeOutTime) //fade out
    {
        fadeImg.gameObject.SetActive(true);
        Color color = fadeImg.color;
        while (color.a < 1f)
        {
            color.a += Time.deltaTime / _fadeOutTime;
            fadeImg.color = color;
            yield return null;
        }
        if (color.a >= 1f) color.a = 1f;
        fadeImg.color = color;
        fadeImg.gameObject.SetActive(false);
    }

    public void StartLoadSceneCor(string _sceneName, float _fadeOutTime, float _fadeInTime, string _playerIn, System.Action callBack = null)
    {
        StartCoroutine(ILoadScene(_sceneName, _fadeOutTime, _fadeInTime, _playerIn, callBack));
    }

    public IEnumerator ILoadScene(string _sceneName, float _fadeOutTime, float _fadeInTime, string _playerIn, System.Action callBack = null)  
    {
        yield return StartCoroutine(IFadeOut(_fadeOutTime));                      // 완전히 페이트 아웃 할 때 까지 대기

        //SceneManager.LoadScene(_sceneName, LoadSceneMode.Additive);
        SceneManager.LoadScene(_sceneName);

        Camera.main.transform.position = new Vector3(0, 0, -10);                    //카메라 좌표 설정
        player.transform.position = new Vector2(0, -3);                             //씬 이동 후 좌표 설정
        player.GetComponent<SpriteRenderer>().sortingOrder = 3;
        locationPlayerIsIn = _playerIn;

        StartCoroutine(IFadeIn(_fadeInTime));

        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == _sceneName);    // 씬 로드될 때 까지 대기

        switch (_playerIn)
        {
            case "LivingRoom":
                BGMManager.instance.PlayBGM(BGM.LivingRoom);
                break;

            case "Jung'sRoom":
                BGMManager.instance.PlayBGM(BGM.JungRoom);
                break;

            case "TreeRoom":
                BGMManager.instance.PlayBGM(BGM.DreamMap);
                break;
        }

        callBack?.Invoke();
    }

    public void MoveJungCor(float _fadeOutTime, float _fadeInTime, string _playerIn, string _map, System.Action callBack = null)
    {
        StartCoroutine(IMoveJung(_fadeOutTime, _fadeInTime, _playerIn, _map, callBack));
    }

    public IEnumerator IMoveJung(float _fadeOutTime, float _fadeInTime, string _playerIn, string _map, System.Action callBack = null)
    {
        yield return StartCoroutine(IFadeOut(_fadeOutTime));                      // 완전히 페이트 아웃 할 때 까지 대기

        Transform place = ObjectManager.GetObject(_map).transform;
        Camera.main.transform.position = new Vector3(place.position.x, place.position.y, -10f);

        float resoulutionX = Screen.width;
        float resoulutionY = Screen.height;
        player.gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(resoulutionX/2f, resoulutionY/2 - 5f, 10));

        player.GetComponent<SpriteRenderer>().enabled = true;
        locationPlayerIsIn = _playerIn;
        map = _map;

        StartCoroutine(IFadeIn(_fadeInTime));

        switch (_map)
        {
            case "LivingRoom":
                BGMManager.instance.PlayBGM(BGM.LivingRoom);
                break;

            case "Jung'sRoom":
                BGMManager.instance.PlayBGM(BGM.JungRoom);
                break;

            case "TreeRoom":
                BGMManager.instance.PlayBGM(BGM.DreamMap);
                break;
        }

        callBack?.Invoke();
    }

    public static bool CheckCondition(ProgressStatus _pro, PlayerAnim _anim)
    {
        return (PlayerScan.instance.progressStatus == _pro && AnimationManager.instance.playerAnim == _anim);
    }
}