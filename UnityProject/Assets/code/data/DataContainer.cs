using Assets.code.data;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JSONUtil))]
public class DataContainer : MonoBehaviour
{
  // St. Peter's
  [SerializeField] string judgedPeopleFileName = "StPetersPleaseTextPeople";
  [SerializeField] string judgeQuestionsFileName = "StPetersPleaseTextQuestions";

  const string DATA_FILE_PREFIX = "data/JSON/";
  Dictionary<int, JudgedPerson> JudgedPeopleCollection = new Dictionary<int, JudgedPerson>();
  Dictionary<int, JudgeQuestion> JudgeQuestionsCollection = new Dictionary<int, JudgeQuestion>();

  void Start()
  {
    DontDestroyOnLoad(this);
    getData();
  }

  void getData()
  {
    JSONUtil jsonUtil = GetComponent<JSONUtil>();
    var judgedPeopleFile = Resources.Load(string.Format("{0}{1}", DATA_FILE_PREFIX, judgedPeopleFileName)) as TextAsset;
    getDataPieces(jsonUtil, judgedPeopleFile, JSONUtil.JSONDataType.JudgedPerson);

    var judgeQuestionsFile = Resources.Load(string.Format("{0}{1}", DATA_FILE_PREFIX, judgeQuestionsFileName)) as TextAsset;
    getDataPieces(jsonUtil, judgeQuestionsFile, JSONUtil.JSONDataType.JudgeQuestion);
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
          JudgedPeopleCollection.Add(judgedPerson.ID, judgedPerson);
        }
        break;
      case JSONUtil.JSONDataType.JudgeQuestion:
        foreach (JudgeQuestion judgeQuestion in dataPieces)
        {
          JudgeQuestionsCollection.Add(judgeQuestion.ID, judgeQuestion);
        }
        break;
    }
  }
}
