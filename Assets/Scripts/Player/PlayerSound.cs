using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void Walk()
    {
        switch (GameManager.instance.locationPlayerIsIn)
        {
            case string _place when (_place == "Jung'sRoom" || _place == "LivingRoom"):
                SoundManager.PlayCappedSFX("Footstep_inside3_re", "Player");
                break;

            case "TreeRoom":
                SoundManager.PlayCappedSFX("Footstep3_dream", "Player");
                break;

            case "Road":
                SoundManager.PlayCappedSFX("Footstep_road", "Player");
                break;
        }
    }
}