using System;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private BackgroundController m_BackgroundController;

    [SerializeField, Range(0.5f, 1.1f)] private float m_SpeedMultiplier;

    private float m_RepeatWidth;
    private Vector3 m_StartPos;
    private float m_Speed = 8.5f;

    private void Start()
    {
        m_StartPos = transform.position;
        m_RepeatWidth = m_BackgroundController.BackgroundCollider.size.x / 2;
    }

    private void Update()
    {
        if (!PlayerController.Instance.IsGameOver)
        {
            // Keep scrolling endlessly
            Vector3 deltaOffset = m_Speed * m_SpeedMultiplier * Time.deltaTime * Vector3.left;
            transform.position += deltaOffset;

            if (transform.position.x < m_StartPos.x - m_RepeatWidth)
            {
                Vector3 newPos = transform.position;
                newPos.x += m_RepeatWidth;
                transform.position = newPos;
            }
        }
    }
}

