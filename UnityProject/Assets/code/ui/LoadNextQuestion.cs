using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LoadNextQuestion : MonoBehaviour
{
  [SerializeField] DisplayQuestionTextFromID displayText = null;
  [SerializeField] GamePanel gamePanel = null;
  DataContainer dataContainer;
  int cachedID = -1;

  IEnumerator Start()
  {
    dataContainer = GameObject.FindWithTag("DataContainer").GetComponent<DataContainer>();
    while (!dataContainer.DataCollected)
    {
      yield return null;
    }
  }

  void Update()
  {
    if(cachedID != displayText.ID)
    {
      cachedID = displayText.ID;
      GetComponent<Button>().interactable = dataContainer.JudgeQuestions.ContainsKey(displayText.ID);
    }
  }

  public void LoadQuestion()
  {
    gamePanel.UpdateDataFromQuestion(dataContainer.JudgeQuestions[displayText.ID]);
  }
}
