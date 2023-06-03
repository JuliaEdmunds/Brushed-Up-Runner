using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<ObjectController> m_Obstacles;
    [SerializeField] private ObjectController m_Brush;

    private void Start()
    {
        PlayerController.Instance.OnGameOver += OnGameOver;

        StartCoroutine(SpawnObstacle());
        StartCoroutine(SpawnBrush());
    }

    private IEnumerator SpawnObstacle()
    {
        Vector3 spawnPos = new(10, 0, 0);
        float spawnInterval = Random.Range(1.5f, 3.5f);

        // TODO: Make sure to stop spawning if gameOver
        int obstacleIndex = Random.Range(0, m_Obstacles.Count);
        GameObject currentObstacle = m_Obstacles[obstacleIndex].gameObject;
        Instantiate(currentObstacle, spawnPos, currentObstacle.transform.rotation);

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

