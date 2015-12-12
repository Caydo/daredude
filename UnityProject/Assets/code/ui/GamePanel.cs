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

    }
}