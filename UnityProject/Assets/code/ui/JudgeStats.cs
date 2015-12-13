using Assets.code.data;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets.code.ui
{
  public class JudgeStats : MonoBehaviour
  {
    public List<JudgedPerson> JudgedPeople = new List<JudgedPerson>();
    public Dictionary<string, int> TagsFromJudged = new Dictionary<string, int>();
    int damnedCount = 0;
    int absolvedCount = 0;

    Dictionary<string, int> absolvedTags = new Dictionary<string, int>();
    Dictionary<string, int> allTags = new Dictionary<string, int>();

    void Start()
    {
      JudgedPeople.Clear();
    }

    public void CompileStats()
    {
      absolvedTags = new Dictionary<string, int>();
      allTags = new Dictionary<string, int>();

      foreach (JudgedPerson person in JudgedPeople)
      {
        if (person.Damned)
        {
          damnedCount++;
        }
        else
        {
          absolvedCount++;
          for (int i = 0; i < person.Tags.Length; i++)
          {
            string tag = person.Tags[i];
            if (!absolvedTags.ContainsKey(tag))
            {
              absolvedTags.Add(tag, 1);
            }
            else
            {
              absolvedTags[tag]++;
            }
          }
        }

        for (int i = 0; i < person.Tags.Length; i++)
        {
          string tag = person.Tags[i];
          if (!allTags.ContainsKey(tag))
          {
            allTags.Add(tag, 1);
          }
          else
          {
            allTags[tag]++;
          }
        }
      }
    }

    public string GetFullStatsText(int index)
    {
      string statsText = string.Empty;
    
      // only do 5 tags at a time
      for(int i = index; i < index + 5; i++)
      {
        KeyValuePair<string, int> kvp = allTags.ElementAt(i);
        string tagName = kvp.Key;
        string totalJudgedAvailableWithTag = kvp.Value.ToString();
        int absolvedCountForTag = (absolvedTags.ContainsKey(kvp.Key)) ? absolvedTags[kvp.Key] : 0;

        statsText += string.Format("{0}: {1}/{2}\n", tagName, absolvedCountForTag.ToString(), totalJudgedAvailableWithTag);
      }

      return statsText;
    }
  }
}
