using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class DataManager : MonoBehaviour
{
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

        string saveData = JsonUtility.ToJson(gamedata);
        File.WriteAllText(Application.dataPath + "/GameData.json", saveData);
        Debug.LogError("saved");
    }

    public static void LoadData()
    {
        string loadData = File.ReadAllText(Application.dataPath + "/GameData.json");
        GameData gamedata = JsonUtility.FromJson<GameData>(loadData);

    }
}

[Serializable]
public class GameData
{
    public ProgressStatus status;                      //진행상황 조건
    public PlayerAnim anim;                            //플레이어 옷 조건
    public int treeStatus;                             //나무상태
    public List<string> doorName = new List<string>(ObjectManager.instance.doorsL.Count);     //도어 이름
    public List<bool> doorStatus = new List<bool>(ObjectManager.instance.doorsL.Count);   //도어 열림 닫힘
    public Slot[] inventoryList = new Slot[Inventory.instance.slotList.Count];
    public string location;                            //플레이어있는 곳( 발자국 용 )
    //string currentMap;                          //현재 플레이어 위치 맵
}
