using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player elements")]
    [SerializeField] private Rigidbody m_PlayerRb;

    [Header("Controls")]
    [SerializeField] private KeyCode m_JumpKey;
    [SerializeField, Range(1, 10)] private float m_JumpForce;

    public event Action OnBrushCollected;

    private void Start()
    {
        Physics.gravity *= 2;
    }

    // Update is called once per frame
    void Update()
    {
        // Allow for easy reset in the testing mode
        // TODO: remove on finish
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Input.GetKeyDown(m_JumpKey))
        {
            m_PlayerRb.AddForce(Vector3.up * m_JumpForce, ForceMode.Impulse);

            // Needs adding jump animation, VFX and sounds

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnObjectController obstacleController = collision.gameObject.GetComponent<SpawnObjectController>();

        if (obstacleController == null)
        {
            return;
        }

        if (obstacleController.SpawnObjectType == ESpawnObjectType.Obstacle)
        {
            // Raise a gameover event
            Debug.Log("Gameover");
        }
        else if (obstacleController.SpawnObjectType == ESpawnObjectType.Brush)
        {
            Destroy(collision.gameObject);
            OnBrushCollected();
        }   
    }
}
