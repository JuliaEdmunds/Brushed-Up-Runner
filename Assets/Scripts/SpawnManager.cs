using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<ObjectController> m_Obstacles;
    [SerializeField] private ObjectController m_Brush;

    // Tweak the values
    private float m_SpawnDelay = 2;
    private float m_SpawnInterval = 2.5f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacles), m_SpawnDelay, m_SpawnInterval);

        //Make brush spawns less frequent & more rabdom
        float brushSpawnDelay = m_SpawnDelay + 2.5f;

        Invoke(nameof(SpawnBrush), brushSpawnDelay);
    }

    private void SpawnObstacles()
    {
        Vector3 spawnPos = new(10, 0, 0);

        // TODO: Make sure to stop spawning if gameOver
        int obstacleIndex = Random.Range(0, m_Obstacles.Count);
        GameObject currentObstacle = m_Obstacles[obstacleIndex].gameObject;
        Instantiate(currentObstacle, spawnPos, currentObstacle.transform.rotation);
    }

    private void SpawnBrush()
    {
        float spawnInterval = Random.Range(3, 10);
        float xPos = 10f;
        float minYPos = 4.5f;
        float maxYPos = 7.7f;

        //Set the random pos for the brush
        Vector3 spawnPos = new(xPos, Random.Range(minYPos, maxYPos), 0);

        Instantiate(m_Brush, spawnPos, m_Brush.transform.rotation);

        Invoke(nameof(SpawnBrush), spawnInterval);
    }
}

