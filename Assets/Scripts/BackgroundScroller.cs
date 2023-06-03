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
    private GameObject m_LastBackground;
    private Vector3 m_StartPos;
    private float m_RepeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentBackground = m_AllBackgrounds[0];
        m_LastBackground = m_CurrentBackground;

        m_CurrentBackground.SetActive(true);

        m_StartPos = transform.position;
        m_RepeatWidth = m_BoxCollider.size.x / 2;

        m_PlayerController.OnBrushCollected += OnBrushCollected;
    }

    private void OnBrushCollected()
    {
        //Disable old bg and replace with a new one
        m_CurrentBackground.SetActive(false);
        ChooseBackground();
        m_CurrentBackground.SetActive(true);
    }

    private void ChooseBackground()
    {
        int index = Random.Range(0, m_AllBackgrounds.Count);
        m_CurrentBackground = m_AllBackgrounds[index];

        // Keep the last background check to make sure we're not switching to the same one
        if (m_LastBackground == m_CurrentBackground)
        {
            ChooseBackground();
        }
        else
        {
            m_LastBackground = m_CurrentBackground;
        }
    }


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
