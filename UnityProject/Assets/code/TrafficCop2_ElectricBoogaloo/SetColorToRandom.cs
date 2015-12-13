using UnityEngine;

public class SetColorToRandom : MonoBehaviour
{
  [SerializeField] Material[] carMaterials = null;
  void Start()
  {
    int randomNumber = Random.Range(0, carMaterials.Length - 1);
    GetComponent<MeshRenderer>().material = carMaterials[randomNumber];
  }
}
