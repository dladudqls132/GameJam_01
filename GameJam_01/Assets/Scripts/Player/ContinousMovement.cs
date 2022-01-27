using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinousMovement : MonoBehaviour
{
    [SerializeField] private GameObject cameraOffset;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference turnAction;
    [SerializeField] private InputActionReference jumpAction;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float jumpForce = 500.0f;

    private Rigidbody rigid;
    private Camera mainCam;
    private XRRig xrRig;
    public bool IsClimb { get; set; }

    private bool IsGrounded => Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z), Vector3.down, 0.15f, 1 << LayerMask.NameToLayer("Enviroment"));

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        mainCam = Camera.main;
        xrRig = GetComponent<XRRig>();

        jumpAction.action.performed += OnJump;
    }

    private void Update()
    {
        xrRig.RotateAroundCameraPosition((Vector3.up * turnAction.action.ReadValue<Vector2>().x).normalized, Mathf.Abs(turnAction.action.ReadValue<Vector2>().x * turnSpeed));
    }

    private void FixedUpdate()
    {
        Vector3 direction = mainCam.transform.rotation * new Vector3(moveAction.action.ReadValue<Vector2>().x, 0, moveAction.action.ReadValue<Vector2>().y).normalized;
        direction.y = 0;

        rigid.useGravity = !IsClimb;
        rigid.velocity = direction.normalized * moveSpeed * moveAction.action.ReadValue<Vector2>().magnitude + new Vector3(0, rigid.velocity.y, 0);
        cameraOffset.GetComponent<Animator>().SetFloat("moveSpeed", moveAction.action.ReadValue<Vector2>().magnitude);
    }

    private void OnJump(InputAction.CallbackContext obj)
    {
        if (!IsGrounded) return;

        rigid.AddForce(Vector3.up * jumpForce * rigid.mass);
    }

    void Walk()
    {

    }
}
