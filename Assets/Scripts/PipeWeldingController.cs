using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PipeWeldingController : MonoBehaviour
{
    public Transform roller;
    public GameObject heater;

    public XRSocketInteractor heaterSocket;
    public GameObject notification;
    public GameObject weldingPipePrefab;

    private Transform leftSide;
    private Transform rightSide;
    private Transform endOfRollerPipe;
    private Vector3 rollerStartPos;
    private GameObject rollerPipe;
    private GameObject staticPipe;
    private Transform endOfStaticPipe;
    private void Start()
    {
        rollerStartPos = roller.position;
        leftSide = heater.transform.Find("LeftSide");
        rightSide = heater.transform.Find("RightSide");
    }

    public void SetRollerPipe(GameObject pipe)
    {
        rollerPipe = pipe;
        endOfRollerPipe = rollerPipe.transform.Find("EndOfPipe");
    }

    public void SetStaticPipe(GameObject pipe)
    {
        staticPipe = pipe;
        endOfStaticPipe = staticPipe.transform.Find("EndOfPipe");
    }
    private void Update()
    {
        
    }
    public void StartWeldingProcess()
    {
        if (rollerPipe != null && endOfRollerPipe != null && staticPipe != null && endOfStaticPipe != null)
        {
            StartCoroutine(HeatingRoutine());
        }
        else
        {
            Debug.LogError("Pipes are not properly assigned for welding process.");
        }
    }
    private IEnumerator HeatingRoutine()
    {
        while (Vector3.Distance(rightSide.position, endOfStaticPipe.position) > 0.01f)
        {
            while (Vector3.Distance(endOfRollerPipe.position, leftSide.position) > 0.01f)
            {
                roller.position = Vector3.MoveTowards(roller.position, leftSide.position, Time.deltaTime * 0.1f);
                yield return null;
            }
            heaterSocket.transform.position += Vector3.back * Time.deltaTime * 0.1f;
            yield return null;
        }

        yield return new WaitForSeconds(10.0f);

        while (Vector3.Distance(roller.position, rollerStartPos) > 0.01f)
        {
            roller.position = Vector3.MoveTowards(roller.position, rollerStartPos, Time.deltaTime * 0.1f);
            yield return null;
        }
        notification.SetActive(true);
        yield return new WaitForSeconds(5.0f);
        notification.SetActive(false);
        while (Vector3.Distance(endOfRollerPipe.position, endOfStaticPipe.position) > 0.01f)
        {
            roller.position = Vector3.MoveTowards(roller.position, endOfStaticPipe.position, Time.deltaTime * 0.1f);
            yield return null;
        }
        Vector3 weldingPosition = endOfStaticPipe.position; // ћесто, где соедин€ютс€ две трубы
        Instantiate(weldingPipePrefab, weldingPosition, Quaternion.identity);
        
        // ”дал€ем старые трубы
        Destroy(rollerPipe);
        Destroy(staticPipe);
    }
}
   

