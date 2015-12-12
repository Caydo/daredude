using UnityEngine;
using System.Collections;
using Assets.code.data;
using UnityEngine.UI;
using System.Collections.Generic;

public class GamePanel : MonoBehaviour
{
    public Image m_judgedPortrait;
    public Text m_judgedName;
    public List<Text> m_judgedTraits;
    public Text m_judgedSpeechText;
    public List<QuestionControl> m_questions;

    void SetJudgedPerson(JudgedPerson person)
    {
        if (person != null)
        {
            //if (m_judgedPortrait != null) m_judgedPortrait.sprite = ?
            if (m_judgedName != null) m_judgedName.text = person.Name;
            UpdateTags(person.Tags);
            if (m_judgedSpeechText != null) m_judgedSpeechText.text = person.StartText;
            UpdateQuestions(person.Questions);
        }
        else
        {
            // clear everything
            if(m_judgedPortrait != null) m_judgedPortrait.sprite = null;
            if (m_judgedName != null) m_judgedName.text = string.Empty;
            UpdateTags(null);
            if (m_judgedSpeechText != null) m_judgedSpeechText.text = string.Empty;
            UpdateQuestions(null);
        }
    }

    private void UpdateTags(string[] tags)
    {
        if (tags != null)
        {
            if (m_judgedTraits != null)
            {
                for (int traitsIndex = 0; traitsIndex < m_judgedTraits.Count; traitsIndex++)
                {
                    if (m_judgedTraits[traitsIndex] != null)
                    {
                        if (tags.Length > traitsIndex)
                        {
                            m_judgedTraits[traitsIndex].text = tags[traitsIndex];
                        }
                        else
                        {
                            m_judgedTraits[traitsIndex].text = string.Empty;
                        }
                    }
                }
            }
        }
        else
        {
            // clear tags
            if (m_judgedTraits != null)
            {
                for (int traitsIndex = 0; traitsIndex < m_judgedTraits.Count; traitsIndex++)
                {
                    if (m_judgedTraits[traitsIndex] != null) m_judgedTraits[traitsIndex].text = string.Empty;
                }
            }
        }
    }
    
    private void UpdateQuestions(int[] questionIDs)
    {
        if (questionIDs != null)
        {
            if (m_questions != null)
            {
                for (int questionIndex = 0; questionIndex < m_questions.Count; questionIndex++)
                {
                    if (questionIDs.Length > questionIndex)
                    {
                        m_questions[questionIndex].DisplayQuestion(questionIDs[questionIndex]);
                    }
                    else
                    {
                        m_questions[questionIndex].DisplayQuestion(-1);
                    }
                }
            }
        }
        else
        {
            //clear questions
            if (m_questions != null)
            {
                for (int questionIndex = 0; questionIndex < m_questions.Count; questionIndex++)
                {
                    if (m_questions[questionIndex] != null) m_questions[questionIndex].m_questionText.text = string.Empty;
                }
            }
        }
    }
}