using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Closet : NPC
{
    [SerializeField] RuntimeAnimatorController pajamaAnim;  //잠옷입은 애니메이터

    public override void Interact()
    {
        bool isWeared = PlayerScan.Instance.isWeared;
        bool eatMed = PlayerScan.Instance.eatMed;
        if (!isWeared && !eatMed)
        {
            PlayerScan.Instance.transform.Find("dress").gameObject.GetComponent<AudioSource>().pitch = 1;
            PlayerScan.Instance.GetComponent<Animator>().runtimeAnimatorController = pajamaAnim;
            PlayerScan.Instance.isWeared = true;
        }
    }
}
