using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
public class Controller : MonoBehaviour
{
    public GameObject root;
    public InputActionProperty velocityProperty;
    public SpringJoint sj;

    public Vector3 Velocity { get; private set; } = Vector3.zero;


    // Update is called once per frame
    void Update()
    {
        Velocity = velocityProperty.action.ReadValue<Vector3>();
    }
    public void Vibration(float amplitude, float duration)
    {
        if(this.GetComponent<XRDirectInteractor>().isSelectActive)
            this.GetComponent<XRDirectInteractor>().SendHapticImpulse(amplitude, duration);
    }
}
