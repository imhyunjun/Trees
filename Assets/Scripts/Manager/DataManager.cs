using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using Newtonsoft.Json;

public class DataManager : DontDestroy<DataManager>
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button loadButton;

    private List<byte[]> byteArrays = new List<byte[]>();

    protected void Awake()
    {
        base.Awake();
        for (int i = 0; i < 6; i++)
            byteArrays.Add(null);
        if(SceneManager.GetActiveScene().name == "Prologue") // Prologue 씬에서 시작해도 작동하기 위한 개발용 코드!!!
        {
            StartCoroutine(FOR_DEV_StartGame());
        }
    }

    public void SaveData()
    {
        Debug.LogError("저장 시작");

        /** 일반 데이터 저장 */
        GameData gamedata = new GameData();

        gamedata.doorName = ObjectManager.instance.doorsL.ConvertAll(x => x.ToString());
        gamedata.doorStatus = ObjectManager.instance.doorsB.ConvertAll(x => x);
        gamedata.location = GameManager.instance.locationPlayerIsIn;
        gamedata.status = PlayerScan.instance.progressStatus;
        gamedata.treeStatus = GameManager.instance.treeGrowStatus;
        gamedata.anim = AnimationManager.instance.playerAnim;
        gamedata.playerPos = GameManager.instance.player.transform.position;
        gamedata.map = GameManager.instance.map;

        //string saveData = JsonUtility.ToJson(gamedata);
        string saveData = JsonConvert.SerializeObject(gamedata, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/GameData.json", saveData);

        /** 인벤토리 데이터 저장*/
        List<InvenData> invenDataList = new List<InvenData>();
        StartCoroutine(SaveInvenData(invenDataList));

        
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
        string loadInvenData = File.ReadAllText(Application.dataPath + "/InvenData.json");
        InvenData[] invenData = JsonConvert.DeserializeObject<InvenData[]>(loadInvenData);
        StartCoroutine(ILoadLastGame(data, invenData));
    }

    IEnumerator ILoadLastGame(GameData data, InvenData[] invenData)        // 게임 데이터 로드 후 실행
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
            Inventory.instance.ApplyToInventory(invenData);

        });
    }

    IEnumerator FOR_DEV_StartGame() // 개발용 코드
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.GameStart();
    }

    IEnumerator SaveInvenData(List<InvenData> invenDataList)
    {
        int idx = 0;

        while (idx < 6)
        {
            bool tempbool = Inventory.instance.slotList[idx].isSlotHasItem;
            //Item tempItem = Inventory.instance.slotList[i].hasItem?.GetComponent<Item>();
            yield return StartCoroutine(ImageToByte(Inventory.instance.slotList[idx].image, idx));
            invenDataList.Add(new InvenData(tempbool, byteArrays[idx]));
            idx++;
            yield return null;
        }

        string invenData = JsonConvert.SerializeObject(invenDataList, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/InvenData.json", invenData);
        Debug.LogError("saved");
    }

    IEnumerator ImageToByte(Image _image, int i)
    {
        yield return new WaitForEndOfFrame();

        Texture newTex = _image.mainTexture;
        Texture2D newTex2D = new Texture2D(newTex.width, newTex.height, TextureFormat.RGBA32, false);
        newTex2D.ReadPixels(new Rect(0, 0, newTex.width, newTex.height), 0, 0);
        newTex2D.Apply();
        byte[] bytes = newTex2D.EncodeToPNG();

        byteArrays[i] = bytes;
        Debug.LogError(byteArrays.Count);
    }

    public Sprite BytesToSprite(byte[] _bytes)
    {
        Texture2D tex = new Texture2D(4, 4);
        tex.LoadImage(_bytes);
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));
    }

}

[Serializable]
public class GameData
{
    public ProgressStatus status;                       //진행상황 조건
    public PlayerAnim anim;                             //플레이어 옷 조건
    public int treeStatus;                              //나무상태
    public List<string> doorName;                       //도어 이름
    public List<bool> doorStatus;                       //도어 열림 닫힘
    public string location;                             //플레이어있는 곳( 발자국 용 )
    public string map;
    public Vector2 playerPos;                           // 플레이어 위치
}

[Serializable]
public class InvenData
{
    public InvenData(bool ishasItem, byte[] hasItemImageToByte)
    {
        this.ishasItem = ishasItem;
        //this.hasItem = hasItem;
        this.hasItemImageToByte = hasItemImageToByte;
    }

    public bool ishasItem { get; set; }
    public Item hasItem { get; set; }
    public byte[] hasItemImageToByte { get; set; }
}




