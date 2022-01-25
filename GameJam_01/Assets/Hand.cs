using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
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
    private Rigidbody _rigid;
    [SerializeField] private Collider[] colliders;
    [SerializeField] private float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        _followTarget = followObject.transform;
        _rigid = GetComponent<Rigidbody>();
        //_rigid.collisionDetectionMode = CollisionDetectionMode.Continuous;
        //_rigid.interpolation = RigidbodyInterpolation.Interpolate;
        _rigid.mass = 20f;

        _rigid.position = _followTarget.position;
        _rigid.rotation = _followTarget.rotation;

        colliders = transform.GetComponentsInChildren<Collider>();

        //anim.SetFloat("AnimationSpeed", animationSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        //AnimateHand();
      
    }

    private void FixedUpdate()
    {
        PhysicsMove();
        //PhysicsMove();
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

    void PhysicsMove()
    {

        // Position
        var positionWithOffset = _followTarget.position + positionOffset;
        var distance = Vector3.Distance(positionWithOffset, transform.position);
        _rigid.velocity = (positionWithOffset - transform.position).normalized * (followSpeed * distance);
        //_rigid.velocity = Vector3.ClampMagnitude(_rigid.velocity, 3.0f);
        //_rigid.position = positionWithOffset;

        // Rotation
        var rotationWithOffset = _followTarget.rotation * Quaternion.Euler(rotationOffset);
        var q = rotationWithOffset * Quaternion.Inverse(_rigid.rotation);
        q.ToAngleAxis(out float angle, out Vector3 axis);
        //_rigid.angularVelocity = axis * (angle * Mathf.Deg2Rad * rotateSpeed);
        //_rigid.angularVelocity = Vector3.Lerp(_rigid.angularVelocity, rotationWithOffset.eulerAngles, Time.deltaTime * 10);
        //_rigid.rotation = _followTarget.rotation;

        //if (Vector3.Distance(transform.position, positionWithOffset) > maxDistance)
        //{
        //    for (int i = 0; i < colliders.Length; i++)
        //    {
        //        colliders[i].enabled = false;
        //    }

        //    isEnabled = false;
        //}
        //else
        //{
        //    if (!isEnabled)
        //    {
        //        for (int i = 0; i < colliders.Length; i++)
        //        {
        //            colliders[i].enabled = true;
        //        }

        //        isEnabled = true;
        //    }
        //}
    }

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
