using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hand : MonoBehaviour
{
    public string handName;
    [SerializeField] private float animationSpeed;
    //private Animator anim;
    SkinnedMeshRenderer mesh;
    //private float gripTarget;
    //private float triggerTarget;
    //private float thumbTouchTarget;
    //private float gripCurrent;
    //private float triggerCurrent;
    //private float thumbTouchCurrent;
    //private string animatorGripParam = "Grip";
    //private string animatorTriggerParam = "Trigger";
    //private string animatorThumbTouchParam = "ThumbTouch";

    // Physics Movement
    [SerializeField] private GameObject followObject;
    [SerializeField] private float followSpeed = 30f;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private Vector3 positionOffset;
    [SerializeField] private Vector3 rotationOffset;
    private Transform _followTarget;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //AnimateHand();
      
    }

    private void FixedUpdate()
    {

    }

    internal void SetGrip(float v)
    {
        //gripTarget = v;
    }

    internal void SetTrigger(float v)
    {
        //triggerTarget = v;
    }

    internal void SetThumbTouch(float v)
    {
        //thumbTouchTarget = v;
    }

    bool isEnabled;

   

    //void AnimateHand()
    //{
    //    if (gripCurrent != gripTarget)
    //    {
    //        gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * animationSpeed);
    //        anim.SetFloat(animatorGripParam, gripCurrent);
    //    }
    //    if (triggerCurrent != triggerTarget)
    //    {
    //        triggerCurrent = Mathf.MoveTowards(triggerCurrent, triggerTarget, Time.deltaTime * animationSpeed);
    //        anim.SetFloat(animatorTriggerParam, triggerCurrent);
    //    }
    //    if (thumbTouchCurrent != thumbTouchTarget)
    //    {
    //        thumbTouchCurrent = Mathf.MoveTowards(thumbTouchCurrent, thumbTouchTarget, Time.deltaTime * animationSpeed);
    //        anim.SetFloat(animatorThumbTouchParam, thumbTouchCurrent);
    //    }
    //}

    public void ToggleVisibility()
    {
        mesh.enabled = !mesh.enabled;

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].enabled = mesh.enabled;
        }
    }
}
