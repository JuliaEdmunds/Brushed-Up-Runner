using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private BoxCollider m_BoxCollider;
    public BoxCollider BackgroundCollider => m_BoxCollider;
    [SerializeField] private List<GameObject> m_AllBackgrounds;

    private GameObject m_CurrentBackground;
    private GameObject m_LastBackground;

    void Start()
    {
        ChooseBackground();

        m_CurrentBackground.SetActive(true);

        PlayerController.Instance.OnBrushCollected += OnBrushCollected;
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

    private void OnDestroy()
    {
        if (PlayerController.IsCreated)
        {
            PlayerController.Instance.OnBrushCollected -= OnBrushCollected;
        }
    }
}
