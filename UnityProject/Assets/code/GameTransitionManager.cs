using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameTransitionManager : MonoBehaviour
{
    public GameObject m_trafficCopUI;
    public GameObject m_stPeterPleaseUI;

    public List<GameObject> m_trafficCopGame;
    public List<GameObject> m_stPeterPleaseGame;

    public List<GameObject> m_trafficCopVehicles = new List<GameObject>();

    void Start()
    {
        StartTrafficCopGame(true);
    }

  [SerializeField] string GameSceneName = "TrafficCop2BackInTheStreets";
    public void ReloadGame()
    {
      SceneManager.LoadScene(GameSceneName);
    }

    public void StartTrafficCopGame(bool startOfGame = false)
    {
        if (!startOfGame)
        {
            StartCoroutine(CR_StartTrafficCopGame());
        }
        else
        {
            ToggleTrafficCop(true);
            ToggleStPeterPlease(false);
            //force panned down
        }
    }

    IEnumerator CR_StartTrafficCopGame()
    {
        //disable st peter
        ToggleStPeterPlease(false);
        //pan down
        yield return new WaitForSeconds(1f);
        //enable traffic cop ui
        ToggleTrafficCop(true);
    }

    public void StartStPeterPleaseGame()
    {
        StartCoroutine(CR_StartStPeterPleaseGame());
    }

    IEnumerator CR_StartStPeterPleaseGame()
    {
        //disable traffic cop
        ToggleTrafficCop(false);
        //pan up
        yield return new WaitForSeconds(1f);
        //enable st peter ui
        ToggleStPeterPlease(true);
    }

    void ToggleTrafficCop(bool enabled)
    {
        m_trafficCopUI.gameObject.SetActive(enabled);
        if (m_trafficCopGame != null)
        {
            for (int i = 0; i < m_trafficCopGame.Count; i++)
            {
                if (m_trafficCopGame[i] != null)
                    m_trafficCopGame[i].gameObject.SetActive(enabled);
            }
        }
        if (!enabled)
        {
            for (int i = 0; i < m_trafficCopVehicles.Count; i++)
            {
                if (m_trafficCopVehicles[i] != null)
                    GameObject.Destroy(m_trafficCopVehicles[i].gameObject);
            }
            m_trafficCopVehicles.Clear();
        }
        else
        {
            StingerText st = Finder.Find<StingerText>("Stinger");
            st.Restart();
        }
    }
    void ToggleStPeterPlease(bool enabled)
    {
        m_stPeterPleaseUI.gameObject.SetActive(enabled);
        if (m_stPeterPleaseGame != null)
        {
            for (int i = 0; i < m_stPeterPleaseGame.Count; i++)
            {
                if (m_stPeterPleaseGame[i] != null)
                    m_stPeterPleaseGame[i].gameObject.SetActive(enabled);
            }
        }
    }
}
