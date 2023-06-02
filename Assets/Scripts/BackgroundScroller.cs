using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private BoxCollider m_BoxCollider;
    [SerializeField] private List<GameObject> m_AllBackgrounds;
    [SerializeField, Range(5, 25)] private float m_Speed;

    [SerializeField] private PlayerController m_PlayerController;

    private GameObject m_CurrentBackground;
    private Vector3 m_StartPos;
    private float m_RepeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentBackground = m_AllBackgrounds[0];
        m_CurrentBackground.SetActive(true);

        m_StartPos = transform.position;
        m_RepeatWidth = m_BoxCollider.size.x / 2;

        m_PlayerController.OnBrushCollected += OnBrushCollected;
    }

    private void OnBrushCollected()
    {
        //Hardcoded value for testing
        m_CurrentBackground.SetActive(false);
        m_CurrentBackground = m_AllBackgrounds[1];
        m_CurrentBackground.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        // Keep scrolling endlessly
        Vector3 deltaOffset = m_Speed * Time.deltaTime * Vector3.left;
        transform.position += deltaOffset;

        if (transform.position.x < m_StartPos.x - m_RepeatWidth)
        {
            transform.position = m_StartPos;
        }
    }

    private void OnDestroy()
    {
        m_PlayerController.OnBrushCollected -= OnBrushCollected;
    }
}
