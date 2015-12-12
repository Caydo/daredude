using Assets.code.data;
using System.Collections.Generic;
using UnityEngine;
public class CastVerdictSelectable : MonoBehaviour
{
  public enum Verdict
  {
    Damnation,
    Absolution
  }
  [SerializeField] Verdict verdict = Verdict.Absolution;
  [SerializeField] Judge gamePanel = null;

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

    gamePanel.JudgeNewPerson();
  }

  void absolvePerson()
  {
    Debug.Log("YOU HAVE PASSED JUDGMENT MORTAL");
  }

  void damnPerson()
  {
    Debug.Log("YOU'RE A JERK! LITERALLY GO TO HELL!");
  }
}
