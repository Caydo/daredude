using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CastVerdictSelectable : MonoBehaviour
{
  public enum Verdict
  {
    Damnation,
    Absolution
  }
  [SerializeField] Verdict verdict = Verdict.Absolution;
  [SerializeField] Judge judge = null;
  [SerializeField] GameObject judgeReportCard = null;
  public GameTransitionManager gameTransitionManager;
  DataContainer dataContainer;

    public List<Button> lockDisableButtons;
    public AudioSource audioSource;

    IEnumerator Start()
  {
    dataContainer = GameObject.FindWithTag("DataContainer").GetComponent<DataContainer>();
    while (!dataContainer.DataCollected)
    {
      yield return null;
    }
  }

  public void CastVerdict()
  {
    if(verdict == Verdict.Absolution)
    {
      StartCoroutine(absolvePerson());
    }
    else
    {
      StartCoroutine(damnPerson());
    }

  }

    IEnumerator absolvePerson()
    {
        LockButtons();
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }

        UnlockButtons();
        //Debug.Log("YOU'RE AWESOME! GO TO HEAVEN!");
        judge.CurrentJudgedPerson.Damned = false;

        if (dataContainer.JudgedPeople.Count > 0)
        {
            gameTransitionManager.StartTrafficCopGame();
        }
        else
        {
            judgeReportCard.SetActive(true);
        }

        judge.JudgeNewPerson();
    }

    IEnumerator damnPerson()
    {
        LockButtons();
        audioSource.Play();
        while (audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }

        UnlockButtons();
        //Debug.Log("YOU'RE A JERK! LITERALLY GO TO HELL!");
        judge.CurrentJudgedPerson.Damned = true;

        if (dataContainer.JudgedPeople.Count > 0)
        {
            gameTransitionManager.StartTrafficCopGame();
        }
        else
        {
            judgeReportCard.SetActive(true);
        }

        judge.JudgeNewPerson();
    }

    void LockButtons()
    {
        if (lockDisableButtons != null)
        {
            for (int i = 0; i < lockDisableButtons.Count; i++)
            {
                if (lockDisableButtons[i] != null)
                    lockDisableButtons[i].interactable = false;
            }
        }
    }

    void UnlockButtons()
    {
        if (lockDisableButtons != null)
        {
            for (int i = 0; i < lockDisableButtons.Count; i++)
            {
                if (lockDisableButtons[i] != null)
                    lockDisableButtons[i].interactable = true;
            }
        }
    }
}
