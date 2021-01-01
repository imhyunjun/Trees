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

    protected void Awake()
    {
        base.Awake();
        if(SceneManager.GetActiveScene().name == "Prologue") // Prologue 씬에서 시작해도 작동하기 위한 개발용 코드!!! 나중에 삭제
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
        gamedata.doorState = ObjectManager.instance.doorsB.ConvertAll(x => x);
        gamedata.itemList = ObjectManager.instance.itemList.ConvertAll(
            x => new ItemData(
                x.gameObject.activeSelf,
                x.isInInventory,
                x.itemName,
                x.GetType(),
                x.canInteractWith,
                x.useType,
                x.transform.position));
        gamedata.invenList = Inventory.instance.slotList.ConvertAll(
            x => new InvenData(
                x.hasItem?.itemName,
                x.hasItem?.GetType(),
                x.IsSlotHasItem));
        gamedata.location = GameManager.instance.locationPlayerIsIn;
        gamedata.status = PlayerScan.instance.progressStatus;
        gamedata.treeStatus = GameManager.instance.treeGrowStatus;
        gamedata.anim = AnimationManager.instance.playerAnim;
        gamedata.playerPos = GameManager.instance.player.transform.position;
        gamedata.map = GameManager.instance.map;
        gamedata.bgm = BGMManager.instance.curBGM;

        //string saveData = JsonUtility.ToJson(gamedata);
        string saveData = JsonConvert.SerializeObject(gamedata, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/GameData.json", saveData);

        StartCoroutine(SaveMapSpriteData());
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

        string mapData = File.ReadAllText(Application.dataPath + "/MapData.json");
        MapSpriteData[] mapDataArray = JsonConvert.DeserializeObject<MapSpriteData[]>(mapData);

        StartCoroutine(ILoadLastGame(data, mapDataArray));
    }

    IEnumerator ILoadLastGame(GameData data, MapSpriteData[] mapDataArray)        // 게임 데이터 로드 후 실행
    {
        SceneManager.LoadScene("Prologue");

        yield return new WaitForSeconds(1f);

        ObjectManager.instance.ApplyItemState(data.itemList); // 아이템 데이터 적용
        Inventory.instance.ApplyInvenState(data.invenList); // 인벤토리 데이터 적용

        GameManager.instance.MoveJungCor(1f, 1f, data.location, data.map, () => // 맵 이동
        {
            GameManager.instance.player.transform.position = data.playerPos;
            PlayerScan.instance.progressStatus = data.status;
            AnimationManager.instance.ChangePlayerAnim(data.anim);
            GameManager.instance.treeGrowStatus = data.treeStatus;
            ObjectManager.instance.ApplyDoorState(data.doorName, data.doorState);
            BGMManager.instance.PlayBGM(data.bgm);
            ApplyMapSprites(mapDataArray);
        });
    }

    IEnumerator FOR_DEV_StartGame() // 개발용 코드
    {
        yield return new WaitUntil(() => GameManager.instance != null);
        GameManager.instance.GameStart();
    }

    IEnumerator SaveMapSpriteData()
    {
        List<MapSpriteData> mapSpriteList = new List<MapSpriteData>();
        foreach(var dic in ObjectManager._mapDicinString)
        {
            mapSpriteList.Add(new MapSpriteData(dic.Key, ImageToByte(dic.Value.GetComponent<SpriteRenderer>().sprite)));
            yield return new WaitForEndOfFrame();
        }

        string mapSaveData = JsonConvert.SerializeObject(mapSpriteList, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/MapData.json", mapSaveData);

        Debug.LogError("저장 끝");
    }

    IEnumerator ImageToByte(Image _image)
    {
        yield return new WaitForEndOfFrame();

        Texture2D newTex2D = (Texture2D)_image.mainTexture;
        Texture2D decompres = newTex2D.DeCompress();
        byte[] bytes = decompres.EncodeToPNG();
    }

    byte[] ImageToByte(Sprite _sprite)
    {
        Texture2D newTex2D = _sprite.texture;
        Texture2D decompres = newTex2D.DeCompress();
        byte[] bytes = decompres.EncodeToPNG();
        return bytes;
    }

    public Sprite BytesToSprite(byte[] _bytes, float _pixelperunit)
    {
        Texture2D tex = new Texture2D(4, 4);
        tex.LoadImage(_bytes);
        return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), _pixelperunit);
    }

    public void ApplyMapSprites(MapSpriteData[] mapData)
    {
        for(int i = 0; i < mapData.Length; i++)
        {
            //ObjectManager.GetObject(mapData[i].mapName).GetComponent<SpriteRenderer>().sprite
            //    = BytesToSprite(mapData[i].mapSpriteTobyte);
            Sprite newSprite = BytesToSprite(mapData[i].mapSpriteTobyte, 60);
            ObjectManager.GetObject(mapData[i].mapName).GetComponent<SpriteRenderer>().sprite = newSprite;
        }
    }

}

[Serializable]
public class GameData
{
    public ProgressStatus status;                       //진행상황 조건
    public PlayerAnim anim;                             //플레이어 옷 조건
    public int treeStatus;                              //나무상태
    public List<string> doorName;                       //도어 이름
    public List<bool> doorState;                       //도어 열림 닫힘
    public List<ItemData> itemList;
    public List<InvenData> invenList;
    public string location;                             //플레이어있는 곳( 발자국 용 )
    public string map;
    public Vector2 playerPos;                           // 플레이어 위치
    public BGM bgm;
}
[Serializable]
public class ItemData
{
    public bool isActive;
    public bool isInInven;
    public string itemName;         //type => string으로 변환
    public Type itemType;           //테스트용 -> 오.json.net사용하니깐 저장 되네요
    public string interactWith;
    public UseType useType;
    public Vector3 position;

    public ItemData(bool isActive, bool isInInven, string itemName, Type itemType, string interactWith, UseType useType, Vector3 position)
    {
        this.isActive = isActive;
        this.isInInven = isInInven;
        this.itemName = itemName;
        this.itemType = itemType;
        this.interactWith = interactWith;
        this.useType = useType;
        this.position = position;
    }
}

public class MapSpriteData
{
    public string mapName;
    public byte[] mapSpriteTobyte;

    public MapSpriteData(string mapName, byte[] mapSpriteTobyte)
    {
        this.mapName = mapName;
        this.mapSpriteTobyte = mapSpriteTobyte;
    }
}

[Serializable]
public class InvenData
{
    public string hasItemName;          //가지고 있는 아이템 이름(string)
    public Type hasItemType;            //가지고 있는 아이템 type
    public bool isHasItem;              //아이템 소유 여부

    public InvenData(string hasItemName, Type hasItemType, bool isHasItem)
    {
        this.hasItemName = hasItemName;
        this.hasItemType = hasItemType;
        this.isHasItem = isHasItem;
    }
}




//나중에 폐기 처분
//[Serializable]
//public class InvenData
//{
   
//    public bool ishasItem;
//    [SerializeField] public Item hasItem;
//    public byte[] hasItemImageToByte;

//    public InvenData(bool ishasItem, Item hasItem, byte[] hasItemImageToByte)
//    {
//        this.ishasItem = ishasItem;
//        this.hasItem = hasItem;
//        this.hasItemImageToByte = hasItemImageToByte;
//    }
//}




