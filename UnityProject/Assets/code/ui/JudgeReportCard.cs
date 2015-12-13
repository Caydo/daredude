using UnityEngine;
using UnityEngine.UI;

namespace Assets.code.ui
{
  public class JudgeReportCard : MonoBehaviour
  {
    [SerializeField] Text reportCardText = null;
    [SerializeField] JudgeStats judgeStats = null;

    public void OnEnable()
    {
      reportCardText.text = judgeStats.GetFullStatsText();
    }
  }
}
