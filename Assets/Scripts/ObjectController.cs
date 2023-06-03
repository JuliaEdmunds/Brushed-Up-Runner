using UnityEngine;

public class ObjectController : MonoBehaviour
{
    private float m_Speed = 5f;
    private float m_XBound = -20f;


    void Update()
    {
        // Keep scrolling endlessly
        if (!PlayerController.Instance.IsGameOver)
        {
            Vector3 deltaOffset = m_Speed * Time.deltaTime * Vector3.left;
            transform.position += deltaOffset;

            // Destroy the obstacle when out of sight
            if (transform.position.x < m_XBound)
            {
                Destroy(gameObject);
            }
        }
    }
}
