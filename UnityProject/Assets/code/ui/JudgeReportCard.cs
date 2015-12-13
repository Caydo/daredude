using UnityEngine;
using UnityEngine.UI;

namespace Assets.code.ui
{
  public class JudgeReportCard : MonoBehaviour
  {
    [SerializeField] Text[] reportCardTextColumns = null;
    [SerializeField] JudgeStats judgeStats = null;
    int incrementAmount = 5;

    public void OnEnable()
    {
      judgeStats.CompileStats();
      for (int i = 0; i < reportCardTextColumns.Length; i++)
      {
        reportCardTextColumns[i].text = judgeStats.GetFullStatsText(i * incrementAmount);
      }
    }
  }
}
