using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CharacterMovementHelper : MonoBehaviour
{
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    private XRRig XRRig;
    private CapsuleCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        XRRig = GetComponent<XRRig>();
        coll = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCharacterController();
    }

    private void UpdateCharacterController()
    {
        if (XRRig == null)
            return;

        var height = Mathf.Clamp(XRRig.cameraInRigSpaceHeight, minHeight, maxHeight);

        Vector3 center = XRRig.cameraInRigSpacePos;
        center.y = height / 2f;

        coll.height = height;
        coll.center = center;
    }
}
