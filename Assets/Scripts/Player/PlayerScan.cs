using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProgressStatus
{
    E_Start,
    E_ChangeClothes,
    E_EatMedicine,
    E_Sleep,
    E_TalkWithPastMom,
    E_TalkWithPastJung,
    E_TalkWithPastFather,
    E_GetBackMirror,
    E_GiveBackMirrorToTree
}

public class PlayerScan : MonoBehaviour
{
    private static PlayerScan instanced;
    public static PlayerScan instance => instanced;

    public RuntimeAnimatorController pajamaAnim;            //잠옷입은 애니메이터

    public int dressStartingPitch = 0; //옷 입는 초기 오디오 소스 음량 조절
    public int drawerStartingPitch = 0; //서랍 여는 초기 오디오 소스 음량 조절
    public int waterGulpStartingPitch = 0; //물 마시는 초기 오디오 소스 음량 조절

    private AudioSource dressStartAudioSource;
    private AudioSource drawerAudioSource;
    private AudioSource bedSource;
    private AudioSource waterGulpSource;

    public Sprite sleepingJung;                         //일단 임시 방편 for 시연회
    public ProgressStatus progressStatus { get; set; }

    private void Awake()
    {
        if (instanced == null)
            instanced = this;
        else
            Destroy(gameObject);
    }

    private void Start() //오디오 초기 음량 설정
    {
        dressStartAudioSource = transform.Find("dress").gameObject.GetComponent<AudioSource>();
        dressStartAudioSource.pitch = dressStartingPitch;

        drawerAudioSource = transform.Find("drawer").gameObject.GetComponent<AudioSource>();
        drawerAudioSource.pitch = drawerStartingPitch;

        waterGulpSource = transform.Find("water").gameObject.GetComponent<AudioSource>();
        waterGulpSource.pitch = waterGulpStartingPitch;

        bedSource = transform.Find("bed").gameObject.GetComponent<AudioSource>();
        bedSource.pitch = 0;

        progressStatus = ProgressStatus.E_Start;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3.5f, LayerMask.GetMask("object"));
            if (hit)
            {
                NPC npc = hit.transform.GetComponent<NPC>();
                if (npc != null) npc.Interact();
            }
        }
    }

}
