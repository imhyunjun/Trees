using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatForDebug : DontDestroy<CheatForDebug>
{
    [Header("버그를 사용하시겠습니까")]
    [Tooltip("버그사용유무")]
    [SerializeField]
    private bool isUsingCheat;
  
    [SerializeField]
    private ProgressStatus progress;

    [SerializeField]
    [ContextMenuItem("Prologue", "ToPrologue")]
    [ContextMenuItem("DreamMap", "ToDreamMap")]
    private string sceneName;

    private enum Items
    {
        BackMirror,
        MarketOwner
    }

    [SerializeField]
    private GameObject[] itemArray;

    [Header("스폰할 아이템 선택하고 스폰 버튼 클릭")]
    [SerializeField]
    private Items items;

    private void Update()
    {
        sceneName = SceneManager.GetActiveScene().name;
        if(isUsingCheat)
        {
            SetCondition();
        }
    }

    void SetCondition()
    { 
         PlayerScan.instance.progressStatus = progress;
    }

    void ToPrologue()
    {
        if (isUsingCheat)
            SceneManager.LoadScene("Prologue");
    }

    void ToDreamMap()
    {
        if (isUsingCheat)
            SceneManager.LoadScene("DreamMap");
    }

    public void SpawItem()
    {
        if(itemArray.Length > (int)items)
        {
            GameObject gameObject = Instantiate(itemArray[(int)items], transform);
            gameObject.transform.position = GameManager.instance.player.transform.position;
            gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("ItemArray에 할당되어 있는지 확인하세요!");
        }

    }
}
