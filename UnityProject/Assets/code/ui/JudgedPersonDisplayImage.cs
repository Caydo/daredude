using Assets.code.data;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class JudgedPersonDisplayImage : MonoBehaviour
{
  const string HEAD_ART_FILE_PREFIX = "art/characters/heads/";

  public void DisplayNewPerson(JudgedPerson person)
  {
    string filePathForImage = string.Empty;
    filePathForImage = string.Format("{0}{1}", HEAD_ART_FILE_PREFIX, person.Head);
    Image image = GetComponent<Image>();
    image.sprite = Resources.Load<Sprite>(filePathForImage);
  }
}
