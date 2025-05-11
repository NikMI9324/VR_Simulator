using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> pipePrefabs; 
    [SerializeField] Transform spawnPoint;         
    [SerializeField] Button spawnButton;           

    private int maxPipes = 2;                     
    private List<GameObject> spawnedPipes = new List<GameObject>();

    void Start()
    {
        spawnButton.onClick.AddListener(SpawnPipe);
        UpdateButtonInteractable();                 
    }

    private void Update()
    {
        UpdateButtonInteractable();                 
    }

    void SpawnPipe()
    {
        if (GetCurrentPipeCount() < maxPipes)
        {
            int randomIndex = Random.Range(0, pipePrefabs.Count);  // ����� ���������� �������
            GameObject newPipe = Instantiate(pipePrefabs[randomIndex], spawnPoint.position, spawnPoint.rotation); // ����� �����
            spawnedPipes.Add(newPipe);  // ���������� ����� ����� � ������ ������������
        }
    }

    int GetCurrentPipeCount()
    {
        return spawnedPipes.Count;  
    }

    void UpdateButtonInteractable()
    {
        spawnButton.interactable = GetCurrentPipeCount() < maxPipes;  
    }
}
