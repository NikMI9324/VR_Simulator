using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
public class PipeAndCoverManager : MonoBehaviour
{
    [SerializeField] XRSocketInteractor pipeSocket1;
    [SerializeField] XRSocketInteractor pipeSocket2;
    [SerializeField] XRSocketInteractor heaterSocket;
    [SerializeField] XRSocketInteractor[] coverSockets;

    [SerializeField] Button heatButton;
    [SerializeField] Button secureButton;
    [SerializeField] Button unsecureButton;

    [SerializeField] XRRayInteractor leftHandRayInteractor;
    [SerializeField] XRRayInteractor rightHandRayInteractor;

    [SerializeField] PipeWeldingController pipeWeldingController;


    private void Start()
    {
        heatButton.interactable = false;
        secureButton.interactable = false;
        unsecureButton.interactable = false;

        pipeSocket1.selectEntered.AddListener(OnObjectPlacedInPipeSocket1);
        pipeSocket2.selectEntered.AddListener(OnObjectPlacedInPipeSocket2);
        heaterSocket.selectEntered.AddListener(OnHeaterObjectPlaced);

        foreach (var socket in coverSockets)
        {
            socket.selectEntered.AddListener(OnObjectPlacedInCoverSocket);
            socket.selectExited.AddListener(OnCoverRemoved);
        }

        secureButton.onClick.AddListener(Secure);
        unsecureButton.onClick.AddListener(Unsecure);
    }

    private void OnObjectPlacedInPipeSocket1(SelectEnterEventArgs args)
    {
        pipeWeldingController.SetRollerPipe(args.interactable.gameObject);
        OnObjectPlaced(args);
    }

    private void OnObjectPlacedInPipeSocket2(SelectEnterEventArgs args)
    {
        pipeWeldingController.SetStaticPipe(args.interactable.gameObject);
        OnObjectPlaced(args);
    }

    private void OnObjectPlacedInCoverSocket(SelectEnterEventArgs args)
    {
        OnObjectPlaced(args);
    }

    private void OnObjectPlaced(SelectEnterEventArgs args)
    {
        if (AllPipesAndCoversPlaced())
        {
            secureButton.interactable = true;
        }
    }

    private void OnHeaterObjectPlaced(SelectEnterEventArgs args)
    {
        if (AllObjectsPlaced())
        {
            heatButton.interactable = true;
        }
    }

    private void Secure()
    {
        // Ѕлокируем взаимодействие как с трубами, так и с крышками
        SetInteractionLayerForCovers(false);
        SetInteractionLayerForPipes(false);
        
        secureButton.interactable = false;
        unsecureButton.interactable = true;
    }

    private void Unsecure()
    {
        // ¬ключаем возможность взаимодействи€ с крышками, но блокируем трубы
        SetInteractionLayerForCovers(true);
        SetInteractionLayerForPipes(false);
        
        secureButton.interactable = true;
        unsecureButton.interactable = false;
    }

    private void OnCoverRemoved(SelectExitEventArgs args)
    {
        secureButton.interactable = false;
        if (AllCoversRemoved())
        {
            SetInteractionLayerForPipes(true);
            Debug.Log("Covers removed, enabling interaction with welding pipe.");
            SetInteractionLayerForWeldingPipes(true);
        }
    }

    
    private void SetInteractionLayerForPipes(bool enable)
    {
        var pipeMask = InteractionLayerMask.GetMask("Pipe");
        if (enable)
        {
            leftHandRayInteractor.interactionLayers |= pipeMask;
            rightHandRayInteractor.interactionLayers |= pipeMask;
        }
        else
        {
            leftHandRayInteractor.interactionLayers &= ~pipeMask;
            rightHandRayInteractor.interactionLayers &= ~pipeMask;
        }
    }

    private void SetInteractionLayerForCovers(bool enable)
    {
        var coverMask = InteractionLayerMask.GetMask("Covers");
        if (enable)
        {
            leftHandRayInteractor.interactionLayers |= coverMask;
            rightHandRayInteractor.interactionLayers |= coverMask;
        }
        else
        {
            leftHandRayInteractor.interactionLayers &= ~coverMask;
            rightHandRayInteractor.interactionLayers &= ~coverMask;
        }
    }
    private void SetInteractionLayerForWeldingPipes(bool enable)
    {
        var weldingPipe = InteractionLayerMask.GetMask("WeldingPipe");
        if(enable)
        {
            leftHandRayInteractor.interactionLayers |= weldingPipe;
            rightHandRayInteractor.interactionLayers |= weldingPipe;
        }
        else
        {
            leftHandRayInteractor.interactionLayers &= ~weldingPipe;
            rightHandRayInteractor.interactionLayers &= ~weldingPipe;
        }
    }

    public bool AllPipesPlaced()
    {
        return pipeSocket1.hasSelection && pipeSocket2.hasSelection;
    }

    public bool AllCoversPlaced()
    {
        foreach (var socket in coverSockets)
        {
            if (!socket.hasSelection)
            {
                return false;
            }
        }
        return true;
    }

    public bool AllPipesAndCoversPlaced()
    {
        return AllPipesPlaced() && AllCoversPlaced();
    }

    public bool AllObjectsPlaced()
    {
        return AllPipesAndCoversPlaced() && heaterSocket.hasSelection;
    }

    public bool AllCoversRemoved()
    {
        foreach (var socket in coverSockets)
        {
            if (socket.hasSelection)
            {
                return false;
            }
        }
        return true;
    }
}

