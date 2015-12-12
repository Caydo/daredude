using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayQuestionTextFromID : MonoBehaviour
{
  public int ID;
  int cachedID = -1;
  DataContainer dataContainer;

  IEnumerator Start()
  {
    dataContainer = GameObject.FindWithTag("DataContainer").GetComponent<DataContainer>();
    while (!dataContainer.DataCollected)
    {
      yield return null;
    }
  }

  public void Update()
  {
    if(cachedID != ID)
    {
      cachedID = ID;
      GetComponent<Text>().text = (dataContainer.JudgeQuestions.ContainsKey(ID)) ? dataContainer.JudgeQuestions[ID].Question : string.Empty;
    }
  }
}
