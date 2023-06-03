using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player elements")]
    [SerializeField] private Rigidbody m_PlayerRb;
    [SerializeField] private AnimationController m_AnimationController;
    [SerializeField] private ParticleSystem m_DirtParticleSystem;

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

            m_AnimationController.Jump();
            m_DirtParticleSystem.Stop();

            // Needs adding jump animation, VFX and sounds

        }
    }

    private void OnCollisionEnter(Collision collision)
    { 
        if (!collision.gameObject.TryGetComponent<ObjectController>(out ObjectController obstacleController))
        {
            // If not colliding with the spawn means Player is on the ground
            m_DirtParticleSystem.Play();
        }
        else if (obstacleController.SpawnObjectType == ESpawnObjectType.Obstacle)
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
