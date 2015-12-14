using UnityEngine;

namespace Assets.code.data
{
  public class JudgePeopleCount : MonoBehaviour
  {
    public int Count = 5;
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
  }
}
