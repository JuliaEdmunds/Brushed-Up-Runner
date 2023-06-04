using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<ObjectController> m_Obstacles;
    [SerializeField] private ObjectController m_Brush;

    [Header("Instructions")]
    [SerializeField] private GameObject m_InstructionPanel;
    [SerializeField] private TextMeshProUGUI m_InstructionText;

    private Vector3 m_ObstacleSpawnPos = new(10, 0, 0);

    private void Start()
    {
        PlayerController.Instance.OnGameOver += OnGameOver;

        StartCoroutine(Tutorial());
    }

    private IEnumerator Tutorial()
    {
        GameObject currentObject;
        //Spawn 1 bucekt
        m_InstructionText.text = "Press Spacebar \r\nto jump";
        m_InstructionPanel.SetActive(true);
        currentObject = m_Obstacles[0].gameObject;
        Instantiate(currentObject, m_ObstacleSpawnPos, currentObject.transform.rotation);

        yield return new WaitForSeconds(4);

        //Spawn 3 buckets
        m_InstructionText.text = "Press Spacebar twice \r\nto double jump";
        currentObject = m_Obstacles[1].gameObject;
        Instantiate(currentObject, m_ObstacleSpawnPos, currentObject.transform.rotation);

        yield return new WaitForSeconds(4);

        // Spawn brush
        m_InstructionText.text = "Collect brushes \r\nto change background";
        currentObject = m_Brush.gameObject;
        Vector3 brushPos = new(10, 4.5f, 0);
        Instantiate(currentObject, brushPos, currentObject.transform.rotation);

        yield return new WaitForSeconds(2.5f);

        m_InstructionPanel.SetActive(false);
        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnBrush());
    }

    private IEnumerator SpawnObstacle()
    {
        float spawnInterval = Random.Range(1.5f, 3.5f);

        // TODO: Make sure to stop spawning if gameOver
        int obstacleIndex = Random.Range(0, m_Obstacles.Count);
        GameObject currentObstacle = m_Obstacles[obstacleIndex].gameObject;
        Instantiate(currentObstacle, m_ObstacleSpawnPos, currentObstacle.transform.rotation);

        yield return new WaitForSeconds(spawnInterval);

        yield return SpawnObstacle();
    }

    private IEnumerator SpawnBrush()
    {
        float spawnInterval = Random.Range(3, 10);
        float xPos = 10f;
        float minYPos = 4.5f;
        float maxYPos = 7.7f;

        //Set the random pos for the brush
        Vector3 spawnPos = new(xPos, Random.Range(minYPos, maxYPos), 0);

        Instantiate(m_Brush, spawnPos, m_Brush.transform.rotation);

        yield return new WaitForSeconds(spawnInterval);

        yield return SpawnBrush();  
    }

    private void OnGameOver()
    {
        StopAllCoroutines();
        PlayerController.Instance.OnGameOver -= OnGameOver;
    }
}

