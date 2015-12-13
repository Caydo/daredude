using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VirtualButton : MonoBehaviour {
    public enum eButtonType
    {
        Fire1,
        Fire2
    }
    public eButtonType m_buttonType;
    public Button m_button;
    public TrafficDirector m_trafficDirector;
    void Start()
    {
        if (m_button != null)
        {
#if UNITY_ANDROID
            m_button.enabled = true;
#else
            m_button.enabled = false;
#endif
        }
    }

    public void OnPress()
    {
        switch (m_buttonType)
        {
            case eButtonType.Fire1:
                m_trafficDirector.OnFire1();
                break;
            case eButtonType.Fire2:
                m_trafficDirector.OnFire2();
                break;
            default:
                break;
        }
    }
}
