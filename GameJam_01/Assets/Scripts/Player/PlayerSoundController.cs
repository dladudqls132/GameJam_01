using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour
{
    void FootStep()
    {
        StartCoroutine(GameManager.Instance.soundManager.AudioPlayOneShotSFX("footstep_snow_1", AudioSourceType.SFX_3D, this.transform.position));
    }
}
