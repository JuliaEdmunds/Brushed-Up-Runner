using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<SpawnObjectController> m_ObstacleControllers;

    // Remove from inspecotr after deciding on the final values
    [SerializeField] private Vector3 m_SpawnPos = new(10, 0, 0);
    [SerializeField] private float m_SpawnDelay = 2;
    [SerializeField] private float m_SpawnInterval = 2.5f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacles), m_SpawnDelay, m_SpawnInterval);
    }

    void SpawnObstacles()
    {
        // TODO: Make sure to stop spawning if gameOver
        int obstacleIndex = Random.Range(0, m_ObstacleControllers.Count);
        GameObject currentObstacle = m_ObstacleControllers[obstacleIndex].gameObject;
        Instantiate(currentObstacle, m_SpawnPos, currentObstacle.transform.rotation);
    }
}

