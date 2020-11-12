using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlcoholBottle : Item
{
    [SerializeField]
    private GameObject _fatherMonster;
    [SerializeField]
    private GameObject _borkenBottle;

    private readonly float maxDistance = 8f;
    public override bool CanUse()
    {
        return Vector3.Distance(GameManager.instance.player.transform.position, _fatherMonster.transform.position) < maxDistance; // 아빠 몬스터와의 거리가 멀면 사용 불가
    }

    public override void FailToUse()
    {
        DialogueManager.instance.PlayDialogue("chapter_16"); // 이걸 어떻게 깨트리지?
    }

    public override void UseItem()
    {
        // 괴물이 술병을 가져가고 깨트리는 애니메이션
        // 술병 깨지는 효과음
        _borkenBottle.SetActive(true); // 일단 동시에 나중에 코루틴으로 변경
    }
}
