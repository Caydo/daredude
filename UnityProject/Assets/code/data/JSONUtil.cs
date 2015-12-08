using System.IO;
using UnityEngine;
using JsonFx.Json;

public class JSONUtil : MonoBehaviour
{
  // Enum used to figure out the type of object to deserialize the file data into.
  public enum JSONDataType
  {
    DataType
  }

  // Get the JSON file at the given path based on the type
  public JSONFileDataPiece[] GetJSONObjectsFromFile(TextAsset textToParse, JSONDataType dataType)
  {
    // We likely don't need this check and it should be changed into an error. If we don't have a text asset to parse then the data file
    // likely doesn't exist and needs to be created.
    if(textToParse)
    {
      // Open the stream reader to pass the file into data, think of this like opening up the file in Notepad to look at it.
      var stringReader = new StringReader(textToParse.text);
      // Pass the entirety of the string found within the file into our data string.
      string data = stringReader.ReadToEnd();
      // Stop reading the file.
      stringReader.Close();

      // ** Crucial Step **
      // Set the reader settings so that we can infer the type when casting later.
      // This is the part that caused hours and hours of Googling!
      JsonReaderSettings readerSettings = new JsonReaderSettings();
      readerSettings.TypeHintName = "__type";
      JsonReader reader = new JsonReader(data, readerSettings);

      // Deserialize the data into an array and pass it back to the data container.
      return castAndGetJSONDataArray(reader, dataType);
    }
    else
    {
      return null;
    }
  }

  // Cast the data received as an array of the class you're expecting then set the array to be returned.  
  JSONFileDataPiece[] castAndGetJSONDataArray(JsonReader reader, JSONDataType dataType)
  {
    // This could likely become simply:
    // return reader.Deserialize<DataPiece[]>();
    // but having an array to assign to can allow things like a switch/case where depending on enum data type you can cast to different
    // classes.

    DataPiece[] cards = reader.Deserialize<DataPiece[]>();
    return cards;
  }
}
