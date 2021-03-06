using Assets.code.data;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(JSONUtil))]
public class DataContainer : MonoBehaviour
{
  // St. Peter's
  [SerializeField] string judgedPeopleFileName = "StPetersPleaseTextPeople";
  [SerializeField] string judgeQuestionsFileName = "StPetersPleaseTextQuestions";

  const string DATA_FILE_PREFIX = "data/JSON/";
  public Dictionary<int, JudgedPerson> AllJudgedPeople = new Dictionary<int, JudgedPerson>();
  public Dictionary<int, JudgedPerson> JudgedPeople = new Dictionary<int, JudgedPerson>();
  public Dictionary<int, JudgeQuestion> JudgeQuestions = new Dictionary<int, JudgeQuestion>();
  public bool DataCollected = false;

  void Start()
  {
    getData();
    GameObject peopleCountObject = GameObject.FindWithTag("JudgePeopleCount");
    int judgePeopleCount = 5;
    if (peopleCountObject != null)
    {
        judgePeopleCount = GameObject.FindWithTag("JudgePeopleCount").GetComponent<JudgePeopleCount>().Count;
    }
    for (int i = 0; i < judgePeopleCount; i++)
    {
      int randomNumberPerson = Random.Range(0, AllJudgedPeople.Count - 1);
      JudgedPerson person = AllJudgedPeople.Values.ElementAt(randomNumberPerson);
      JudgedPeople.Add(person.ID, person);
      AllJudgedPeople.Remove(person.ID);
    }
  }

  void getData()
  {
    JSONUtil jsonUtil = GetComponent<JSONUtil>();
    var judgedPeopleFile = Resources.Load(string.Format("{0}{1}", DATA_FILE_PREFIX, judgedPeopleFileName)) as TextAsset;
    getDataPieces(jsonUtil, judgedPeopleFile, JSONUtil.JSONDataType.JudgedPerson);

    var judgeQuestionsFile = Resources.Load(string.Format("{0}{1}", DATA_FILE_PREFIX, judgeQuestionsFileName)) as TextAsset;
    getDataPieces(jsonUtil, judgeQuestionsFile, JSONUtil.JSONDataType.JudgeQuestion);
    DataCollected = true;
  }

  void getDataPieces(JSONUtil jsonUtil, TextAsset dataFile, JSONUtil.JSONDataType dataType)
  {
    JSONFileDataPiece[] dataPieces = jsonUtil.GetJSONObjectsFromFile(dataFile, dataType);
    if (dataPieces != null)
    {
      populateData(dataPieces, dataType);
    }
    dataPieces = null;
  }

  void populateData(JSONFileDataPiece[] dataPieces, JSONUtil.JSONDataType dataType)
  {
    switch (dataType)
    {
      case JSONUtil.JSONDataType.JudgedPerson:
        foreach (JudgedPerson judgedPerson in dataPieces)
        {
          AllJudgedPeople.Add(judgedPerson.ID, judgedPerson);
        }
        break;
      case JSONUtil.JSONDataType.JudgeQuestion:
        foreach (JudgeQuestion judgeQuestion in dataPieces)
        {
          JudgeQuestions.Add(judgeQuestion.ID, judgeQuestion);
        }
        break;
    }
  }
}
