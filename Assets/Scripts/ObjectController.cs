using UnityEngine;

public class ObjectController : MonoBehaviour
{
    [SerializeField] private ObjectType m_ObjectType;

    private const float SPEED = 7.5f;
    private const float X_BOUND = -20f;


    void Update()
    {
        // Keep scrolling endlessly
        if (!PlayerController.Instance.IsGameOver)
        {
            Vector3 deltaOffset = SPEED * Time.unscaledDeltaTime * Vector3.left;
            transform.position += deltaOffset;

            // If it's a brush then keep rotating it
            if (m_ObjectType.SpawnObjectType == ESpawnObjectType.Brush)
            {
                float degreesPerSecond = 30;
                transform.Rotate(new Vector3(0, degreesPerSecond, 0) * Time.unscaledDeltaTime);
            }

            // Destroy the obstacle when out of sight
            if (transform.position.x < X_BOUND)
            {
                Destroy(gameObject);
            }
        }
    }
}
