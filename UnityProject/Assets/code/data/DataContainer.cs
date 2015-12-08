using System.Collections.Generic;
using UnityEngine;

public class DataContainer : MonoBehaviour
{
  // The path (folders and file name, no extension) for where the desired data lives. I've put it in Resources as that seems
  // quick (most of my data files have been small) and there doesn't seem to be perfomance hits due to it.
  [SerializeField] string dataFilePath = "data/JSON/DataFile";
  // Rather than using GetComponent this is a serialized field in case JSONUtil is desired on another game object.
  [SerializeField] JSONUtil jsonUtil = null;
  // The object to load the .json file into
  TextAsset dataFile;
  // The array that the JSON Util will fill with objects it deserializes
  JSONFileDataPiece[] dataPieces;
  // A helper dictionary with a key currently set to whatever the number is (e.g. an ID or something maybe) to the value
  // of the actual deserialized data piece. This can make it easier for other classes and objects to get the data when needed.
  // Though, this isn't strictly needed. You could make the array public or protected depending on your needs and how you want
  // to parse through the data.
  Dictionary<int, DataPiece> DataCollection = new Dictionary<int, DataPiece>();
  
  void Start()
  {
    // If the data needs to live throughout the life of the application (mine has thus far) then the object this is attached to shouldn't
    // be destroyed when changing scenes.
    DontDestroyOnLoad(this);
    // Find the file from the file path and put it into a text asset object
    dataFile = Resources.Load(dataFilePath) as TextAsset;
    // Get the data via JSON Util. This could all be put into start, but in my case I've been getting different data pieces/types so
    // I wanted to split them up into different functions.
    getData();
  }

  void getData()
  {
    // Populate the data array. This doesn't have to be "dataPieces" it can be anything. Items, spells, enemies, whatever.
    // It takes in an enum in order to figure out what to deserialize the file into. This can be useful for having a bunch of
    // different data files (e.g. one for enemies, one for items, one for spells) that all have the JSONFileDataPiece base
    // class, but have different fields (enemies have HP whereas items have cooldown times and spells have critical hit percentage or
    // something.
    dataPieces = jsonUtil.GetJSONObjectsFromFile(dataFile, JSONUtil.JSONDataType.DataType);

    // Likely this null check can be removed or used to thrown an error or warning. If the array is null that means that no data was
    // found. This means that the file probably doesn't exist and you should create the data.
    if (dataPieces != null)
    {
      // Populate the helper dictionary with the appropriate key and value. As I said before, you might not need this, but it can be
      // useful to either put this here or pass the now deserialized data to another class or something so it's wherever you  need it.
      foreach (DataPiece pieceOfData in dataPieces)
      {
        DataCollection.Add(pieceOfData.Number, pieceOfData);
        Debug.Log
        (
          string.Format("Found a piece of data with the following info:\nName - {0},\nNumber - {1},\nPoints - {2},\nPower - {3},\nChild Array's First Element Name - {4}",
          pieceOfData.Name, pieceOfData.Number, pieceOfData.Points, pieceOfData.Power, pieceOfData.AdditionalDataPieces[0].Name)
        );
      }
    }
  }
}
