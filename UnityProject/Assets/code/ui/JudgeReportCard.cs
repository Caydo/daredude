using Assets.code.data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.code.ui
{
  public class JudgeReportCard : MonoBehaviour
  {
    [SerializeField] Text AbsolvedText = null;
    [SerializeField] Text DamnedText = null;
    [SerializeField] Text[] reportCardTextColumns = null;
    [SerializeField] Text savedCarsText = null;
    [SerializeField] JudgeStats judgeStats = null;

    [SerializeField] Image absolvedPiImage;

    int incrementAmount = 5;
    public void OnEnable()
    {
      int damnedCount = 0;
      int absolvedCount = 0;
      foreach(JudgedPerson person in judgeStats.JudgedPeople)
      {
        if(person.Damned)
        {
          damnedCount++;
        }
        else
        {
          absolvedCount++;
        }
      }

      absolvedPiImage.fillAmount = (float)absolvedCount / (float)(damnedCount + absolvedCount);

      DamnedText.text = string.Format("Damned Souls: {0}", damnedCount);
      AbsolvedText.text = string.Format("Absolved Souls: {0}", absolvedCount);

      judgeStats.CompileStats();
      for (int i = 0; i < reportCardTextColumns.Length; i++)
      {
        reportCardTextColumns[i].text = judgeStats.GetFullStatsText(i * incrementAmount);
      }

      savedCarsText.text = string.Format("Mortal Vehicles Saved From Judgment: {0}", Finder.Find<SpawnController>("GameController").successVehicles());
    }
  }
}
