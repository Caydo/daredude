using System.IO;
using UnityEngine;
using JsonFx.Json;
using Assets.code.data;

public class JSONUtil : MonoBehaviour
{
  public enum JSONDataType
  {
    DataType,
    JudgedPerson,
    JudgeQuestion
  }

  public JSONFileDataPiece[] GetJSONObjectsFromFile(TextAsset textToParse, JSONDataType dataType)
  {
    var stringReader = new StringReader(textToParse.text);
    string data = stringReader.ReadToEnd();
    stringReader.Close();

    // ** Crucial Step **
    // Set the reader settings so that we can infer the type when casting later.
    // This is the part that caused hours and hours of Googling!
    JsonReaderSettings readerSettings = new JsonReaderSettings();
    readerSettings.TypeHintName = "__type";
    JsonReader reader = new JsonReader(data, readerSettings);

    return castAndGetJSONDataArray(reader, dataType);
  }

  JSONFileDataPiece[] castAndGetJSONDataArray(JsonReader reader, JSONDataType dataType)
  {
    JSONFileDataPiece[] data = null;
    switch (dataType)
    {
      case JSONDataType.DataType:
        data = reader.Deserialize<DataPiece[]>();
        break;
      case JSONDataType.JudgedPerson:
        data = reader.Deserialize<JudgedPerson[]>();
        break;
      case JSONDataType.JudgeQuestion:
        data = reader.Deserialize<JudgeQuestion[]>();
        break;
    }

    return data;
  }
}
