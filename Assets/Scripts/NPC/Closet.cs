using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : NPC
{
    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        PlayerAnim skinstatus = AnimationManager.instance.playerAnim;
        if ((status == ProgressStatus.E_Start || status == ProgressStatus.E_JungGotShocked) && skinstatus == PlayerAnim.E_Uniform)
        {
            SoundManager.PlaySFX("clothes");
            AnimationManager.instance.ChangePlayerAnim(PlayerAnim.E_Pajama);
            if (status == ProgressStatus.E_Start) PlayerScan.instance.progressStatus = ProgressStatus.E_ChangeClothes;
            else if (status == ProgressStatus.E_JungGotShocked) PlayerScan.instance.progressStatus = ProgressStatus.E_ChangeClothes2;
            AnimationManager.instance.playerAnim = PlayerAnim.E_Pajama;
        }
        else if (GameManager.CheckCondition(ProgressStatus.E_Chapter2Start, PlayerAnim.E_Pajama))
        {
            SoundManager.PlaySFX("clothes");                                        //기획서에 없지만 일단 추가
            AnimationManager.instance.ChangePlayerAnim(PlayerAnim.E_Uniform);
            AnimationManager.instance.playerAnim = PlayerAnim.E_Uniform;
            ObjectManager.GetObject<LivingRoomDoor>().isOpened = true;
            ObjectManager.GetObject<FrontDoor>().isOpened = true;
            ObjectManager.GetObject<ToSuperMarketDoor>().isOpened = false;
            ObjectManager.GetObject<ToSchoolDoor>().isOpened = true;
        }
    }
}
