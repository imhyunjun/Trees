using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Fridge : MonoBehaviour
{
    [SerializeField]
    private GameObject alcoholBottle;                                //임시용
    
    public void OnMouseDown()
    {
        Debug.Log("설마");
        //쩅그랑 소리 추가 이곳 아니면 알코올의 GetItem함수에 추가
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashNCard)         //카드를 가진상태면
        {
            alcoholBottle.SetActive(true);
            Inventory.instance.GetItemInSlot(alcoholBottle);                             //술을 가져오고
            PlayerScan.instance.progressStatus = ProgressStatus.E_GetAlcholBottle;      //술을 가진 상태로 변경
        }
    }
}
