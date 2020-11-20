using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBottle : Item
{
    int tabCount = 0;       //정이가 찌른 횟수

    public override void UseItem()      
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;

        if (canInteractWith == "FatherMonster" && status == ProgressStatus.E_JungWannaKillFather)
        {
            isInInventory = true;           //사용해도 그대로 남아있기
            //찌르기
            //괴물 으어억 하는 사운드
            //피를 흘리다? 이미지 변경?
            tabCount++;                     //횟수를 올린다
            Debug.Log($"{tabCount}번 찌름");
            if (tabCount == 10)
            {
                StartCoroutine(ICrying());
                tabCount = 0;
            }
        }
        else if(canInteractWith == "Tree" && status == ProgressStatus.E_JungWannaKillFather)
        {
            GameManager.instance.treeGrowStatus++;     // 나무 성장 
        }

    }

    public override bool CanUse()
    {
        return base.CanUse();
        //조건?, 공격 범위?
    }

    private IEnumerator ICrying()
    {
        //정 앉기
        //정 우는 사운드
        PlayerMove.canMove = false;//못움직이게
        yield return new WaitForSeconds(3f);    //3초 후에
        PlayerMove.canMove = true;
        //정 다시 서기 -> 플레이어 이미지 변경 함수 나중에 만들기( 기본, 잠옷, 손 들고 있는거 등등), 플레이어 아니라 다른것도 많아지면 생각..
        //사운드 오프 혹은 사운드 자체가 3초 지속하게 하기
    }

}
