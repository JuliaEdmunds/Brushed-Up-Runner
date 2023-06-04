using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private ObjectType m_ObjectType;

    private float m_Speed = 7.5f;
    private float m_XBound = -20f;


    void Update()
    {
        // Keep scrolling endlessly
        if (!PlayerController.Instance.IsGameOver)
        {
            Vector3 deltaOffset = m_Speed * Time.deltaTime * Vector3.left;
            transform.position += deltaOffset;

            // If it's a brush then keep rotating it
            if (m_ObjectType.SpawnObjectType == ESpawnObjectType.Brush)
            {
                float degreesPerSecond = 30;
                transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.deltaTime);
            }

            // Destroy the obstacle when out of sight
            if (transform.position.x < m_XBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
