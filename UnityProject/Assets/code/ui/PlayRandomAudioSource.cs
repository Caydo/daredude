using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayRandomAudioSource : MonoBehaviour
{
    public List<AudioSource> m_audioSources;

    public void PlayRandom()
    {
        if(m_audioSources != null)
        {
            int index = Random.Range(0, m_audioSources.Count);
            if(m_audioSources[index] != null) m_audioSources[index].Play();
        }
    }
}
