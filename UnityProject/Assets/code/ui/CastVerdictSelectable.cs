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
