using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuestionControl : MonoBehaviour
{
    public Text m_questionText;

    public void DisplayQuestion(int questionID)
    {
        if(questionID == -1)
        {
            m_questionText.text = string.Empty;
            gameObject.SetActive(false);
            return;
        }
        //todo
        gameObject.SetActive(true);
        m_questionText.text = questionID.ToString();
    }
}
