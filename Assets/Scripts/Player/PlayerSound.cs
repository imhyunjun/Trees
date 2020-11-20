using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public void Walk()
    {
        switch (GameManager.instance.locationPlayerIsIn)
        {
            case "LivingRoom":
            case "JungRoom":
                SoundManager.PlayCappedSFX("Footstep_inside3_re", "Player");
                break;

            case "DreamMap":
                SoundManager.PlayCappedSFX("Footstep3_dream", "Player");
                break;
        }
    }
}