using Assets.code.data;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.code.ui
{
  public class JudgeStats : MonoBehaviour
  {
    public List<JudgedPerson> JudgedPeople = new List<JudgedPerson>();
    public Dictionary<string, int> TagsFromJudged = new Dictionary<string, int>();
    void Start()
    {
      JudgedPeople.Clear();
    }

    public string GetFullStatsText()
    {
      string statsText = string.Empty;
      Dictionary<string, int> tagsToCount = new Dictionary<string, int>();
      Dictionary<string, int> damnedTagToCount = new Dictionary<string, int>();
      Dictionary<string, int> dictToAddTo = new Dictionary<string, int>();

      foreach (JudgedPerson person in JudgedPeople)
      {
        for(int i = 0; i < person.Tags.Length; i++)
        {
          string tag = person.Tags[i];
          if(!tagsToCount.ContainsKey(tag))
          {
            tagsToCount.Add(tag, 1);
          }
          else
          {
            tagsToCount[tag]++;
          }
        }
      }

      foreach(KeyValuePair<string, int> kvp in tagsToCount)
      {
        string tagName = kvp.Key;
        string totalJudgedAvailableWithTag = kvp.Value.ToString();
        int discoveredTags = (TagsFromJudged.ContainsKey(kvp.Key)) ? TagsFromJudged[kvp.Key] : 0;

        statsText += string.Format("{0}: {1}/{2}\n", kvp.Key, discoveredTags.ToString(), totalJudgedAvailableWithTag);
      }

      return statsText;
    }
  }
}
