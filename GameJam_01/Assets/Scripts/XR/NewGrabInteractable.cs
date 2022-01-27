using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NewGrabInteractable : XRGrabInteractable
{
    Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        if (isSelected)
        {
            if(transform.parent != null)
                rigid.velocity = (this.transform.parent.position - this.transform.position).normalized * 100 * Vector3.Distance(this.transform.parent.position, this.transform.position);
        }
       // rigid.angularVelocity = Vector3.Lerp(rigid.angularVelocity, Vector3.zero, Time.deltaTime * 10);
    }

    Transform tempParent;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        if (args.interactor is XRDirectInteractor)
        {
            this.transform.rotation = args.interactor.transform.rotation;

            tempParent = this.transform.parent;
            this.transform.parent = args.interactor.transform;

            rigid.constraints = RigidbodyConstraints.FreezeRotation;

            base.OnSelectEntered(args);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        if (args.interactor is XRDirectInteractor)
        {
            this.transform.SetParent(tempParent);
            tempParent = null;
            rigid.constraints = RigidbodyConstraints.None;

            base.OnSelectExited(args);
        }
    }
}
