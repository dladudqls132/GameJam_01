using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRSocketInteractorTag : XRSocketInteractor
{
    [SerializeField] private string targetTag;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        //tempColl = args.interactor.selectTarget.gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in args.interactable.colliders)
        {
            c.isTrigger = true;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        foreach (Collider c in args.interactable.colliders)
        {
            c.isTrigger = false;
        }

        //tempColl = null;
    }

    public override bool CanSelect(XRBaseInteractable interactable)
    {
        return base.CanSelect(interactable) && interactable.CompareTag(targetTag);
    }
}
