using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportation;
    [SerializeField] private GameObject rightTeleportation;

    [SerializeField] private InputActionProperty leftActive;
    [SerializeField] private InputActionProperty rightActive;

    [SerializeField] private InputActionProperty leftCancel;
    [SerializeField] private InputActionProperty rightCancel;

    [SerializeField] private XRRayInteractor leftRay;
    [SerializeField] private XRRayInteractor rightRay;

    private void Update()
    {
        leftTeleportation.SetActive(leftActive.action.ReadValue<float>() > 0.1f && leftCancel.action.ReadValue<float>() == 0 /*&& !isLeftRayHovering*/);
        rightTeleportation.SetActive(rightActive.action.ReadValue<float>() > 0.1f && rightCancel.action.ReadValue<float>() == 0 /*&& !isRightRayHovering*/);
    }

    
    
}