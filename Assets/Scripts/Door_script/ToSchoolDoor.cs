using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSchoolDoor : Door
{
    private void Start()
    {
        isOpened = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (PlayerScan.instance.progressStatus == ProgressStatus.E_GetCashNCard)
            DialogueManager.instance.PlayDialogue("chapter_0_6");  // 우선 슈퍼에 가야해
    }

    public override void AfterPlayerArrived()
    {
        if(PlayerScan.instance.progressStatus == ProgressStatus.E_Chapter2Start)
            GameManager.instance.MoveJungCor(3, 3, "ClassRoom", () => StartCoroutine(EnterClassRoom()));
    }

    private IEnumerator EnterClassRoom()
    {
        //교실에서 정이 엎드려 있는 장면 전환
        //웅성우성한 사운드 추가
        yield return new WaitForSeconds(3f);
        //말풍선창 번갈아가면서 나오기
        DialogueManager.instance.PlayDialogue("chapter_2_4");       //선생님에게 희망을 품는 정..
        //대화가 끝난 후에
        //누군가 정에게 다가온다
        //대사 말풍선창 뜨기 - 말풍선 뜨는 함수를 어떻게 해야할것 ..같은데
        //정이 일어나는 애니메이션 감이 안옴
        DialogueManager.instance.PlayDialogue("chapter_2_5");       //교무실로 가면 되겠지.
        PlayerScan.instance.progressStatus = ProgressStatus.E_TeacherCallJung;
        PlayerMove.canMove = true;
        ObjectManager.GetObject<SchoolDoor>().isOpened = false;
    }
}
