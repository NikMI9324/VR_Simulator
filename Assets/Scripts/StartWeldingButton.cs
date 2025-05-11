using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartWeldingButton : MonoBehaviour
{
    public Button startButton;
    public PipeWeldingController weldingController;

    void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        weldingController.StartWeldingProcess();
    }
}
