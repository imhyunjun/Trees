using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : NPC
{
    [SerializeField]
    private RuntimeAnimatorController pajamaAnim;  //잠옷 입은 애니메이터

    public override void Interact()
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_Start)
        {
            PlayerScan.instance.transform.Find("dress").gameObject.GetComponent<AudioSource>().pitch = 1;
            PlayerScan.instance.GetComponent<Animator>().runtimeAnimatorController = pajamaAnim;
            PlayerScan.instance.progressStatus++;
        }
    }
}
