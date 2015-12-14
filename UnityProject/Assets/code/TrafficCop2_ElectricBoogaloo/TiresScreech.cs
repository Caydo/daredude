using UnityEngine;
using System.Collections;
using System.Linq;

public class TiresScreech : MonoBehaviour
{
    public AudioSource m_audioSource;
    public Vehicle m_vechilce;
    
    void OnTriggerEnter(Collider other)
    {
        var otherTiresScreech = other.gameObject.GetComponent<TiresScreech>();
        if (otherTiresScreech != null) // ignore collision with other things than vehicles
        {
            var otherStateMachine = otherTiresScreech.m_vechilce.transform.Cast<Transform>()
                .Select(t => t.GetComponent<StateMachine>())
                .Where(t => t != null)
                .First();
            if (otherStateMachine.CurrentState == "Go")
            {
                m_audioSource.Play();
            }
        }
    }
}
