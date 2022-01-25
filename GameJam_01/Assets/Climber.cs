using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class Climber : MonoBehaviour
{
    public static XRBaseController climbingHand;
    private ContinousMovement continousMovement;
    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        continousMovement = GetComponent<ContinousMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(climbingHand)
        {
            //continousMovement.enabled = false;
            Climb();
            continousMovement.IsClimb = true;
        }
        else
        {
            //continousMovement.enabled = true;
            continousMovement.IsClimb = false;
        }
    }

    void Climb()
    {
        Vector3 dir = Vector3.up * climbingHand.GetComponent<Controller>().Velocity.y + Camera.main.transform.right * climbingHand.GetComponent<Controller>().Velocity.x + Camera.main.transform.forward * climbingHand.GetComponent<Controller>().Velocity.z;

        rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z) + (-dir);
    }
}
