using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{
    public Animation anim;

    public float speed = 1;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animation>();

        if (anim == null) return;

        foreach (AnimationState state in anim)
        {
            state.speed = speed;
        }
        anim.Play();
    }

}
