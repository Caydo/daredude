using System.Collections;
using UnityEngine;
public class CastVerdictSelectable : MonoBehaviour
{
  public enum Verdict
  {
    Damnation,
    Absolution
  }
  [SerializeField] Verdict verdict = Verdict.Absolution;
  [SerializeField] Judge judge = null;
  [SerializeField] GameObject judgeReportCard = null;
  public GameTransitionManager gameTransitionManager;
  DataContainer dataContainer;

  IEnumerator Start()
  {
    dataContainer = GameObject.FindWithTag("DataContainer").GetComponent<DataContainer>();
    while (!dataContainer.DataCollected)
    {
      yield return null;
    }
  }

  public void CastVerdict()
  {
    if(verdict == Verdict.Absolution)
    {
      absolvePerson();
    }
    else
    {
      damnPerson();
    }

    if(dataContainer.JudgedPeople.Count > 0)
    {
      gameTransitionManager.StartTrafficCopGame();
     }
    else
    {
      judgeReportCard.SetActive(true);
    }

    judge.JudgeNewPerson();
  }

  void absolvePerson()
  {
    //Debug.Log("YOU'RE AWESOME! GO TO HEAVEN!");
    judge.CurrentJudgedPerson.Damned = false;
  }

  void damnPerson()
  {
    //Debug.Log("YOU'RE A JERK! LITERALLY GO TO HELL!");
    judge.CurrentJudgedPerson.Damned = true;
  }
}
