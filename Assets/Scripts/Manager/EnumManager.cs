
// 게임 진행 상태
public enum ProgressStatus
{
    E_Start,
    E_ChangeClothes,
    E_EatMedicine,
    E_Sleep,
    E_TalkWithTreeFirstTime,
    E_TalkWithPastMom,
    E_TalkWithPastJung,
    E_TalkWithPastDad,
    E_GetBackMirror,
    E_GiveBackMirrorToTree,
    E_TalkWithCurrentDad,
    E_GetCashNCard,
    E_GetAlcholBottle,
    E_PayedDone,
    E_ErrandFinished,
    E_JungWannaKillFather,
    E_Chapter2Start,
    E_TeacherCallJung,
    E_JungGotShocked,                    //이제 조건이름 뭐라해야할지 모르겠다     // 보라 : ㅋㅋㅋㅋㅋㅋ큐ㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅠㅋㅋㅋㅋ
    E_ChangeClothes2,
    E_EatMedicine2,
    E_FaceScarToButterfly,
    E_EndMirrorRoom,
    E_TalkWithTreeAfterMirror
}

//플레이어 옷 조건이 너무 많아질것 같아서 좀 뺐어요.. 경우의 수로..
public enum PlayerAnim { E_Uniform, E_Pajama };

public enum UseType // 아이템 사용 타입
{
    Interact,       // 어떤 대상과 상호작용하면서 사용
    Immediately,    // 그냥 클릭하면 즉시 사용
    Repeat          // 반복 사용( 깨진 술병 )          
}
