using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyCanvas : DontDestroy<DontDestroyCanvas>
{
    public void SaveGame()
    {
        DataManager.instance.SaveData();
    }
}
