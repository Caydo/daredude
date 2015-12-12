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
  Dictionary<int, JudgedPerson> judgedPeople = new Dictionary<int, JudgedPerson>();

  void Start()
  {
    judgedPeople = GameObject.FindWithTag("DataContainer").GetComponent<DataContainer>().JudgedPeople;
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

    judgeNewPerson();
  }

  void absolvePerson()
  {
    Debug.Log("YOU HAVE PASSED JUDGMENT MORTAL");
  }

  void damnPerson()
  {
    Debug.Log("YOU'RE A JERK! LITERALLY GO TO HELL!");
  }

  void judgeNewPerson()
  {
    // get a random index then get the corresponding judged person and set them
    int randomNumber = Random.Range(0, judgedPeople.Count - 1);
    //SetJudgedPerson(judgedPeople[randomNumber]);
  }
}
