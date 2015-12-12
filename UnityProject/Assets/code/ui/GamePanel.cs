using UnityEngine;
using Assets.code.data;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using Assets.code.ui;

public class GamePanel : MonoBehaviour
{
  public Image m_judgedPortrait;
  public Text m_judgedName;
  public List<Text> m_judgedTraits;
  public Text m_judgedSpeechText;
  public List<DisplayQuestionTextFromID> m_questions;
  int[] cachedQuestionIDs = null;
  DataContainer dataContainer;
  [SerializeField] DisplayJudgedPersonTags displayJudgedTags = null;

  IEnumerator Start()
  {
    dataContainer = GameObject.FindWithTag("DataContainer").GetComponent<DataContainer>();
    while(!dataContainer.DataCollected)
    {
      yield return null;
    }

    JudgeNewPerson();
  }

  public void JudgeNewPerson()
  {
    displayJudgedTags.Tags.Clear();
    JudgedPerson person = getRandomPerson();
    if (person != null)
    {
      //if (m_judgedPortrait != null) m_judgedPortrait.sprite = ?
      if (m_judgedName != null) m_judgedName.text = person.Name;
      if (m_judgedSpeechText != null) m_judgedSpeechText.text = person.StartText;
      updateQuestions(person.Questions);
    }
    else
    {
      // clear everything
      if(m_judgedPortrait != null) m_judgedPortrait.sprite = null;
      if (m_judgedName != null) m_judgedName.text = string.Empty;
      if (m_judgedSpeechText != null) m_judgedSpeechText.text = string.Empty;
      updateQuestions(null);
    }
  }

  JudgedPerson getRandomPerson()
  {
    int randomNumber = Random.Range(0, dataContainer.JudgedPeople.Count - 1);
    JudgedPerson person = dataContainer.JudgedPeople[randomNumber];
    return person;
  }
      
  void updateQuestions(int[] questionIDs)
  {
    for(int questionIndex = 0; questionIndex < m_questions.Count; questionIndex++)
    {
      m_questions[questionIndex].ID = questionIDs[questionIndex];
    }

    cachedQuestionIDs = questionIDs;
  }

  public void UpdateData(JudgeQuestion judgeQuestion)
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
