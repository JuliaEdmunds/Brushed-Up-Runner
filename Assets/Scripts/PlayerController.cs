using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

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
    public static bool IsCreated => Instance != null;

    public bool IsGameOver { get; private set; }

    public int Score { get; private set; }

    public bool IsNewBestScore { get; private set; }

    private int m_JumpCounter = 0;

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
        Score = 0;

        StartCoroutine(UpdateScore());
    }


    void Update()
    {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        if (Input.GetKeyDown(m_JumpKey))
        {
            TryJump();
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            TryJump();
        }
#endif
    }

    private void TryJump()
    {
        if (m_JumpCounter < 2 && !IsGameOver)
        {
            m_JumpCounter++;

            float currentJumpForce = m_JumpCounter == 2 ? (m_JumpForce * 0.75f) : m_JumpForce;

            m_PlayerRb.AddForce(Vector3.up * currentJumpForce, ForceMode.Impulse);

            m_AnimationController.Jump();
            m_AudioController.PlayJumpSound();
            m_DirtParticleSystem.Stop();
        }
    }

    private IEnumerator UpdateScore()
    {
        WaitForSeconds waitTime = new WaitForSeconds(1f);

        while (true)
        {
            yield return waitTime;
            Score += 1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
         if (!collision.gameObject.TryGetComponent<ObjectType>(out ObjectType objectType))
        {
            // If not colliding with the spawn means Player is on the ground
            m_JumpCounter = 0;
            m_DirtParticleSystem.Play();
        }
        else if (objectType.SpawnObjectType == ESpawnObjectType.Obstacle)
        {
            EndGame();
        }
        else if (objectType.SpawnObjectType == ESpawnObjectType.Brush)
        {
            Score += 5;
            m_AudioController.PlayBrushCollectedSound();
            Destroy(collision.gameObject);
            OnBrushCollected();
        }
    }

    private void EndGame()
    {
        StopAllCoroutines();

        m_SmokeParticleSystem.Play();
        m_AudioController.PlayDieSound();
        
        m_DirtParticleSystem.Stop();
        m_AnimationController.Die();
        m_AudioController.TurnOffSound();

        if (Score > PlayerScoreHelper.GetBestScore())
        {
            PlayerScoreHelper.SetBestScore(Score);
            IsNewBestScore = true;
        }

        IsGameOver = true;
        OnGameOver();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}
