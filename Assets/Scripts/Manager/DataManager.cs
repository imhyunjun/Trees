using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class DataManager : DontDestroy<DataManager>
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button loadButton;

    protected void Awake()
    {
        base.Awake();
        if(SceneManager.GetActiveScene().name == "Prologue") // Prologue 씬에서 시작해도 작동하기 위한 개발용 코드!!!
        {
            StartCoroutine(FOR_DEV_StartGame());
        }
    }

    public void SaveData()
    {
        Debug.LogError("저장 시작");
        GameData gamedata = new GameData();

        Inventory.instance.slotList.CopyTo(gamedata.inventoryList);
        gamedata.doorName = ObjectManager.instance.doorsL.ConvertAll(x => x.ToString());
        gamedata.doorStatus = ObjectManager.instance.doorsB.ConvertAll(x => x);
        gamedata.location = GameManager.instance.locationPlayerIsIn;
        gamedata.status = PlayerScan.instance.progressStatus;
        gamedata.treeStatus = GameManager.instance.treeGrowStatus;
        gamedata.anim = AnimationManager.instance.playerAnim;
        gamedata.map = GameManager.instance.map;
        gamedata.playerPos = GameManager.instance.player.transform.position;

        string saveData = JsonUtility.ToJson(gamedata);
        File.WriteAllText(Application.dataPath + "/GameData.json", saveData);
        Debug.LogError("saved");
    }

    public void OnClickStartButton()
    {
        startButton.interactable = false;
        loadButton.interactable = false;
        SoundManager.PlaySFX("Click_3");
        StartCoroutine(IStartNewGame());
    }
    IEnumerator IStartNewGame()             // 게임 처음부터 시작
    {
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Prologue");
        BGMManager.instance.PlayBGM(BGM.LivingRoom);
        yield return new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.GameStart();
    }

    public void OnClickLoadLastGameButton()
    {
        startButton.interactable = false;
        loadButton.interactable = false;
        SoundManager.PlaySFX("Click_3");

        string loadData = File.ReadAllText(Application.dataPath + "/GameData.json");
        GameData data = JsonUtility.FromJson<GameData>(loadData);
        StartCoroutine(ILoadLastGame(data));
    }

    IEnumerator ILoadLastGame(GameData data)        // 게임 데이터 로드 후 실행
    {
        SceneManager.LoadScene("Prologue");

        yield return new WaitForSeconds(1f);

        GameManager.instance.MoveJungCor(1f, 1f, data.location, data.map, () =>
        {
            GameManager.instance.player.transform.position = data.playerPos;
            PlayerScan.instance.progressStatus = data.status;
            AnimationManager.instance.ChangePlayerAnim(data.anim);
            GameManager.instance.treeGrowStatus = data.treeStatus;
            ObjectManager.instance.ApplyDoorStatus(data.doorName, data.doorStatus);
            // 인벤토리
        });
    }

    IEnumerator FOR_DEV_StartGame() // 개발용 코드
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.GameStart();
    }
}

[Serializable]
public class GameData
{
    public ProgressStatus status;                      //진행상황 조건
    public PlayerAnim anim;                            //플레이어 옷 조건
    public int treeStatus;                             //나무상태
    //public List<string> doorName = new List<string>(ObjectManager.instance.doorsL.Count);     //도어 이름
    //public List<bool> doorStatus = new List<bool>(ObjectManager.instance.doorsL.Count);   //도어 열림 닫힘
    public List<string> doorName;     //도어 이름
    public List<bool> doorStatus;   //도어 열림 닫힘
    public Slot[] inventoryList = new Slot[Inventory.slotCount];
    public string location;                            //플레이어있는 곳( 발자국 용 )
    public string map;                          //현재 플레이어 위치 맵
    public Vector2 playerPos;                    // 플레이어 위치
}
