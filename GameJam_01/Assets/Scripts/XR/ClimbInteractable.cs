using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ClimbInteractable : XRBaseInteractable
{
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactor is XRDirectInteractor)
        {
            Climber.climbingHand = args.interactor.GetComponent<XRBaseController>();

            args.interactor.GetComponent<Controller>().sj.transform.position = args.interactor.transform.position;
            args.interactor.GetComponent<Controller>().sj.connectedBody = args.interactor.GetComponent<Controller>().root.GetComponent<Rigidbody>();
            args.interactor.GetComponent<Controller>().sj.maxDistance = args.interactor.GetComponent<Controller>().sj.connectedAnchor.y / 3;
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (args.interactor is XRDirectInteractor)
        {
            if(Climber.climbingHand && Climber.climbingHand.name == args.interactor.name)
            {
                Climber.climbingHand = null;
            }

            args.interactor.GetComponent<Controller>().sj.connectedBody = null;
        }
    }
}
