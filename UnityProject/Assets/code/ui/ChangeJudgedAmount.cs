using Assets.code.data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.code.ui
{
  public class ChangeJudgedAmount : MonoBehaviour
  {
    void Start()
    {
      ChangeAmount();
    }
    public void ChangeAmount()
    {
      int count = (int)GetComponent<Slider>().value;
      GameObject.FindGameObjectWithTag("JudgePeopleCount").GetComponent<JudgePeopleCount>().Count = count;
    }
  }
}
