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
    Debug.Log("CASTING HOLY VENGEANCE OR SALVATION UPON THEE!");
    // get a random index then get the corresponding judged person and set them
    int randomNumber = Random.Range(0, judgedPeople.Count - 1);
    //SetJudgedPerson(judgedPeople[randomNumber]);
  }
}
