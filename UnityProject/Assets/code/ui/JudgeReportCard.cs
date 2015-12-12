using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.code.ui
{
  [RequireComponent(typeof(Image))]
  [RequireComponent(typeof(Button))]
  public class JudgeReportCard : MonoBehaviour
  {
    [SerializeField] Text reportCardText = null;
    [SerializeField] JudgeStats judgeStats = null;

    public void DisplayReportCard()
    {
      GetComponent<Image>().enabled = true;
      GetComponent<Button>().enabled = true;
      reportCardText.text = judgeStats.GetFullStatsText();
      reportCardText.enabled = true;
    }

    /// <summary>
    /// Called via event system
    /// </summary>
    public void GoToScene(string sceneToGoTo)
    {
      SceneManager.LoadScene(sceneToGoTo);
    }
  }
}
