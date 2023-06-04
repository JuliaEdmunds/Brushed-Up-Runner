using System.Collections;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_JumpSound;
    [SerializeField] private AudioClip m_DieSound;
    [SerializeField] private AudioClip m_BrushCollectedSound;

    public void PlayJumpSound()
    {
        m_AudioSource.PlayOneShot(m_JumpSound);
    }

    public void PlayBrushCollectedSound()
    {
        m_AudioSource.PlayOneShot(m_BrushCollectedSound);
    }

    public void PlayDieSound()
    {
        m_AudioSource.PlayOneShot(m_DieSound);
    }

    public void TurnOffSound()
    {
        StartCoroutine(DoTurnOffSound());
    }

    private IEnumerator DoTurnOffSound()
    {
        yield  return new WaitForSeconds(3.5f);

        m_AudioSource.Stop();
    }
}

