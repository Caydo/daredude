using UnityEngine;
using UnityEngine.UI;

namespace Assets.code.data
{
  public class JudgePeopleCount : MonoBehaviour
  {
    public int Count = 5;
    [SerializeField] Slider slider = null;
    void Start()
    {
      if(GameObject.FindGameObjectsWithTag("JudgePeopleCount").Length > 1)
      {
        Destroy(this);
      }
      else
      {
        DontDestroyOnLoad(this);
      }
    }

    public void UpdateCount()
    {
      Count = (int)slider.value;
    }
  }
}
