using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class XRGrabInteractableTwoAttach : XRGrabInteractable
{
    public Transform leftAttach;
    public Transform rightAttach;
    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (args.interactorObject.transform.CompareTag("Left Hand"))
        {
            attachTransform.position = leftAttach.position;
            attachTransform.rotation = leftAttach.rotation;
        }
        else if (args.interactorObject.transform.CompareTag("Right Hand"))
        {
            attachTransform.position = rightAttach.position;
            attachTransform.rotation = rightAttach.rotation;
        }

    }
}
