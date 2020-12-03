using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : DontDestroy<AnimationManager>
{
    [SerializeField]
    private List<RuntimeAnimatorController> animatorList;  //잠옷입은 애니메이터

    public PlayerAnim playerAnim { get; set; } = PlayerAnim.E_Uniform;
  
    public void ChangePlayerAnim(PlayerAnim _player_skin)
    {
        GameManager.instance.player.GetComponent<Animator>().runtimeAnimatorController = animatorList[(int)_player_skin];
        playerAnim = _player_skin;
    }

}
