using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.code.ui
{
  public class JudgeReportCard : MonoBehaviour
  {
    [SerializeField] Image backgroundImage = null;
    [SerializeField] Text reportCardText = null;
    [SerializeField] Button button = null;
    [SerializeField] Image image = null;
    [SerializeField] Text buttonText = null;
    [SerializeField] JudgeStats judgeStats = null;

    public void DisplayReportCard()
    {
      backgroundImage.enabled = true;
      image.enabled = true;
      button.enabled = true;
      reportCardText.text = judgeStats.GetFullStatsText();
      reportCardText.enabled = true;
      buttonText.text = "DONE";
    }
  }
}
