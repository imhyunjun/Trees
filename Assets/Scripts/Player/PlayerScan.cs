using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScan : MonoBehaviour
{
    private static PlayerScan instance;
    public static PlayerScan Instance => instance;

    //float MaxDistance = 10f;
    public bool isWeared = false; //처음에는 교복 안 입고 있음.
    public bool eatMed = false;//처음에는 수면제 안 먹음. 일단 스태틱~~~~~ 구동만 되게 시연회..!

    public RuntimeAnimatorController pajamaAnim;            //잠옷입은 애니메이터

    public int dressStartingPitch = 0; //옷 입는 초기 오디오 소스 음량 조절
    public int drawerStartingPitch = 0; //서랍 여는 초기 오디오 소스 음량 조절
    public int waterGulpStartingPitch = 0; //물 마시는 초기 오디오 소스 음량 조절

    private AudioSource dressStartAudioSource;
    private AudioSource drawerAudioSource;
    private AudioSource bedSource;
    private AudioSource waterGulpSource;

    public Sprite sleepingJung;                         //일단 임시 방편 for 시연회

    public int countOfJungMetTree = 0;

    private void Awake()
    {
        if (instance == null)
            instance = this;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3.5f, LayerMask.GetMask("object"));
            if (!hit) return;
            NPC npc = hit.transform.GetComponent<NPC>();
            if (npc != null) npc.Interact();
        }
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 3.5f, LayerMask.GetMask("object"));
    //        Debug.DrawRay(transform.position, 3.5f * transform.up, Color.green);
    //        if (!hit) return;

    //        // 약도 안 먹고 옷도 안 입은 경우
    //        if (isWeared==false && eatMed==false) 
    //        {
    //            if (hit.collider.name == "closet")
    //            {
    //                dressStartAudioSource.pitch = 1;
    //                gameObject.GetComponent<Animator>().runtimeAnimatorController = pajamaAnim;
    //                isWeared = true; //옷 입음.
    //            }

    //            else if(hit.collider.name != "Tree")
    //            {
    //                StartCoroutine(DialogueManager.Instance.IContinueDialogue(7, 7));
    //            }

    //        }

    //        // 약은 안 먹고 옷은 입은 경우
    //        else if (isWeared == true && eatMed == false)
    //        {
    //            if (hit.collider.name == "Bed")
    //            {
    //                StartCoroutine(DialogueManager.Instance.IContinueDialogue(8, 8));

    //            }

    //            if(hit.collider.name == "drawer")
    //            {
    //                drawerAudioSource.pitch = 2;
    //                DialogueManager.Instance.OnOffDialogueImage(true); //yes, no 버튼 있을 경우 반복문 사용 금지. stack에 쌓여서 못 빠져 나옴.
    //                StartCoroutine(DialogueManager.Instance.PlayText(9));

    //                YButton.SetActive(true);
    //                NButton.SetActive(true);
    //            }
    //        }

    //        //약도 먹고 옷도 입은 경우
    //        else if (isWeared == true && eatMed==true)
    //        {
    //            if (hit.collider.name == "Bed")
    //            {
    //                bedSource.pitch = 3;
    //                hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = sleepingJung;
    //                gameObject.GetComponent<SpriteRenderer>().sortingOrder = -1;
    //                StartCoroutine(GameManager.Instance.IFadeOut(5f));
    //                StartCoroutine(GameManager.Instance.ILoadScene("DreamMap", 5f, "DreamMap"));
    //                GameManager.Instance.gameSceneProcedure++;
    //            }
    //        }

    //        //프롤로그 나무를 처음 마주침
    //        if (countOfJungMetTree == 0 && hit.collider.name == "Tree")
    //        {
    //            StartCoroutine(DialogueManager.Instance.IContinueDialogue(11, 22, DialogueManager.Instance.currentProcedureIndexS, DialogueManager.Instance.currentProcedureIndexE));
    //        }
    //    }
    //}

}
