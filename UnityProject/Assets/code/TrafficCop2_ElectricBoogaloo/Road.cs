using UnityEngine;
using System.Collections;


public enum TrafficPolicy
{
    Stop = 0, Go = 1//, Yield?
}

public class Road : MonoBehaviour {
    public MeshRenderer m_stopWalkMeshRenderer;
    public Material m_stopMat;
    public Material m_goMat;

    private TrafficPolicy _currentPolicy;
    public TrafficPolicy currentPolicy
    {
        get { return _currentPolicy; }
        set
        {
            _currentPolicy = value;
            if(m_stopWalkMeshRenderer != null)
            {
                switch (_currentPolicy)
                {
                    case TrafficPolicy.Stop:
                        m_stopWalkMeshRenderer.material = m_stopMat;
                        break;
                    case TrafficPolicy.Go:
                        m_stopWalkMeshRenderer.material = m_goMat;
                        break;
                }
            }
        }
    }

    void Start()
    {
        if (m_stopWalkMeshRenderer != null)
        {
            m_stopWalkMeshRenderer.material = m_stopMat;
        }
    }
}
