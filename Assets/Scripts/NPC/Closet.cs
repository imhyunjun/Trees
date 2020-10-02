using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : NPC
{
    [SerializeField]
    private RuntimeAnimatorController pajamaAnim;  //잠옷입은 애니메이터

    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if (status == ProgressStatus.E_Start)
        {
            SoundManager.PlaySFX("clothes");
            PlayerScan.instance.GetComponent<Animator>().runtimeAnimatorController = pajamaAnim;
            PlayerScan.instance.progressStatus++;
        }
    }
}
