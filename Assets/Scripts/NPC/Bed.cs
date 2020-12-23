using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bed : NPC
{
    [SerializeField]
    private Sprite sleepingJung;     //일단 임시 방편 for 시연회
    [SerializeField]
    private SpriteRenderer jungRoom;
    [SerializeField]
    private Sprite nightJungRoomSprite;
    private Sprite bedDefault;
    private Sprite JungRoomDefault;

    private void Awake()
    {
        bedDefault = gameObject.GetComponent<SpriteRenderer>().sprite;
        JungRoomDefault = jungRoom.sprite;
    }

    public override void Interact()
    {
        ProgressStatus status = PlayerScan.instance.progressStatus;
        if(status == ProgressStatus.E_Start || status == ProgressStatus.E_JungGotShocked)
        {
            DialogueManager.instance.PlayDialogue("prologue_5");
        }
        else if(status == ProgressStatus.E_ChangeClothes || status == ProgressStatus.E_ChangeClothes2)
        {
            DialogueManager.instance.PlayDialogue("prologue_6");
        }
        else if(status == ProgressStatus.E_EatMedicine)
        {
            GoToDreamMap();
            PlayerScan.instance.progressStatus = ProgressStatus.E_Sleep;
        }
        else if(status == ProgressStatus.E_ErrandFinished)
        {
            GoToDreamMap();
        }
        else if (status == ProgressStatus.E_Chapter2Start)
        {
            DialogueManager.instance.PlayDialogue("chapter_2_2");       //늦기전에 학교에 가자
        }
        else if(status == ProgressStatus.E_EatMedicine2)
        {
            GoToDreamMap(() =>
            {
                DialogueManager.instance.PlayDialogue("chapter_2_8", false, () => {  // 나무 : 오늘 하루는 어땠어?  ~~
                    ObjectManager.GetObject<MirrorRoomDoor>().isOpened = true;
                    SoundManager.PlaySFX("door-open"); // 문열리는 효과음
                });
            });
        }
    }

    private void GoToDreamMap(System.Action afterMove = null)
    {
        PlayerMove.canMove = false;
        SoundManager.PlaySFX("lying-on-bed");
        GetComponent<SpriteRenderer>().sprite = sleepingJung;
        PlayerScan.instance.GetComponent<SpriteRenderer>().enabled = false;
        GameManager.instance.MoveJungCor(5f, 2f, "TreeRoom", () => 
        {
            PlayerMove.canMove = true;
            afterMove?.Invoke();
        });
    }

    public void ChangeJungRoomToNight()
    {
        jungRoom.sprite = nightJungRoomSprite;
        GetComponent<SpriteRenderer>().sprite = bedDefault;
    }

    public void ChangeJungRoomToMorning()
    {
        jungRoom.sprite = JungRoomDefault;
        GetComponent<SpriteRenderer>().sprite = bedDefault;
    }

}
