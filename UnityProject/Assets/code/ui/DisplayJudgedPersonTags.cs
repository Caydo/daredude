using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Assets.code.ui
{
  public class DisplayJudgedPersonTags : MonoBehaviour
  {
    [SerializeField] GameObject TagPrefab = null;
    [SerializeField] JudgeStats judgeStats = null;
    List<GameObject> TagObjects = new List<GameObject>();
    public List<string> Tags;
    int cachedTagsCount = -1;

    void Update()
    {
      if (Tags.Count != cachedTagsCount)
      {
        cachedTagsCount = Tags.Count;
        foreach(GameObject tagObject in TagObjects)
        {
          Destroy(tagObject);
        }

        TagObjects.Clear();

        foreach (string tag in Tags)
        {
          createTag(tag);
        }
      }
    }

    void createTag(string tag)
    {
      GameObject newTagObject = (GameObject)Instantiate(TagPrefab, transform.position, Quaternion.identity);
      newTagObject.transform.SetParent(transform);
      newTagObject.transform.localScale = Vector3.one;
      newTagObject.transform.localEulerAngles = Vector3.zero;
      newTagObject.GetComponent<Text>().text = tag;
      TagObjects.Add(newTagObject);

      if(!judgeStats.TagsFromJudged.ContainsKey(tag))
      {
        judgeStats.TagsFromJudged.Add(tag, 1);
      }
      else
      {
        judgeStats.TagsFromJudged[tag]++;
      }
    }
  }
}
