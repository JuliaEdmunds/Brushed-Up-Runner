using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player elements")]
    [SerializeField] private Rigidbody m_PlayerRb;
    [SerializeField] private AnimationController m_AnimationController;
    [SerializeField] private ParticleSystem m_DirtParticleSystem;
    [SerializeField] private ParticleSystem m_SmokeParticleSystem;
    [SerializeField] private AudioController m_AudioController;

    [Header("Controls")]
    [SerializeField] private KeyCode m_JumpKey;
    [SerializeField, Range(1, 10)] private float m_JumpForce;

    public event Action OnBrushCollected;
    public event Action OnGameOver;

    public static PlayerController Instance { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

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
            m_AudioController.PlayJumpSound();
            m_DirtParticleSystem.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
         if (!collision.gameObject.TryGetComponent<ObjectType>(out ObjectType objectType))
        {
            // If not colliding with the spawn means Player is on the ground
            m_DirtParticleSystem.Play();
        }
        else if (objectType.SpawnObjectType == ESpawnObjectType.Obstacle)
        {
            EndGame();
        }
        else if (objectType.SpawnObjectType == ESpawnObjectType.Brush)
        {
            m_AudioController.PlayBrushCollectedSound();
            Destroy(collision.gameObject);
            OnBrushCollected();
        }
    }

    private void EndGame()
    {
        IsGameOver = true;
        OnGameOver();

        m_SmokeParticleSystem.Play();
        m_AudioController.PlayDieSound();
        
        m_DirtParticleSystem.Stop();
        m_AnimationController.Die();
        m_AudioController.TurnOffSound();

        Physics.gravity /= 2;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
