using Assets.code.data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.code.ui
{
  public class JudgeStats : MonoBehaviour
  {
    public List<JudgedPerson> JudgedPeople = new List<JudgedPerson>();
    void Start()
    {
      JudgedPeople.Clear();
    }

    public string GetFullStatsText()
    {
      string statsText = "A;SDLFJKAL;SDFKJAS;LFJ";

      foreach(JudgedPerson person in JudgedPeople)
      {

      }

      return statsText;
    }
  }
}
