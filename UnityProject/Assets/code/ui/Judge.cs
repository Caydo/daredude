using UnityEngine;
using Assets.code.data;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Assets.code.ui;

public class Judge : MonoBehaviour
{
  public Image m_judgedPortrait;
  public Text m_judgedName;
  public Text m_judgedSpeechText;
  public List<DisplayQuestionTextFromID> m_questions;
  int[] cachedQuestionIDs = null;
  DataContainer dataContainer;
  [SerializeField] DisplayJudgedPersonTags displayJudgedTags = null;
  [SerializeField] JudgedPersonDisplayImage[] JudgedPersonBodyParts = null;
  List<JudgedPerson> JudgedSouls = new List<JudgedPerson>();

  IEnumerator Start()
  {
    dataContainer = GameObject.FindWithTag("DataContainer").GetComponent<DataContainer>();
    while(!dataContainer.DataCollected)
    {
      yield return null;
    }

    JudgeNewPerson();
  }

  JudgedPerson getRandomPerson()
  {
    JudgedPerson person = null;
    List<int> availableIDs = new List<int>();
    foreach(KeyValuePair<int, JudgedPerson> kvp in dataContainer.JudgedPeople)
    {
      availableIDs.Add(kvp.Value.ID);
    }

    int randomNumber = Random.Range(0, availableIDs.Count - 1);
    person = dataContainer.JudgedPeople[availableIDs[randomNumber]];
    
    if(!JudgedSouls.Contains(person))
    {
      JudgedSouls.Add(person);
      dataContainer.JudgedPeople.Remove(person.ID);
      return person;
    }

    return null;
  }

  public void JudgeNewPerson()
  {
    displayJudgedTags.Tags.Clear();
    JudgedPerson person = getRandomPerson();
    if (person != null)
    {
      if (m_judgedName != null) m_judgedName.text = person.Name;
      if (m_judgedSpeechText != null) m_judgedSpeechText.text = person.StartText;

      for (int questionIndex = 0; questionIndex < m_questions.Count; questionIndex++)
      {
        m_questions[questionIndex].ID = person.Questions[questionIndex];
      }

      cachedQuestionIDs = person.Questions;

      foreach (JudgedPersonDisplayImage bodypart in JudgedPersonBodyParts)
      {
        bodypart.DisplayNewPerson(person);
      }
    }
    else
    {
      Debug.LogError("YOU RAN OUT OF PEOPLE TO JUDGE!!!!");
    }
  }
      
  // a question button was pressed, update relevant UI
  public void UpdateDataFromQuestion(JudgeQuestion judgeQuestion)
  {
    // update questions
    List<int> newCachedIds = new List<int>();
    for (int questionIndex = 0; questionIndex < m_questions.Count; questionIndex++)
    {
      int cachedID = cachedQuestionIDs[questionIndex];
      if (dataContainer.JudgeQuestions.ContainsKey(cachedID))
      {
        int newID = dataContainer.JudgeQuestions[cachedQuestionIDs[questionIndex]].Next;
        m_questions[questionIndex].ID = newID;
        newCachedIds.Add(newID);
      }
      else
      {
        newCachedIds.Add(-1);
      }
    }

    cachedQuestionIDs = newCachedIds.ToArray();

    // update answer
    m_judgedSpeechText.text = judgeQuestion.Answer;

    // update tags
    if (judgeQuestion.TagReveal != string.Empty)
    {
      if(!displayJudgedTags.Tags.Contains(judgeQuestion.TagReveal))
      {
        displayJudgedTags.Tags.Add(judgeQuestion.TagReveal);
      }
    }
  }
}
