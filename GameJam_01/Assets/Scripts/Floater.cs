using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rigid;
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public int floaterCount = 1;
    public float waterDrag = 5f;
    public float waterAngularDrag = 2f;
    Waves waves;

    private void Awake()
    {
        waves = FindObjectOfType<Waves>();
    }

    private void FixedUpdate()
    {
        //rigid.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = waves.GetHeight(transform.position);
        if(transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight -transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rigid.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rigid.AddForce(displacementMultiplier * -rigid.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rigid.AddTorque(displacementMultiplier * -rigid.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
