using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToSceneButton : MonoBehaviour
{
  public void GoToScene(string sceneToGoTo)
  {
    SceneManager.LoadScene(sceneToGoTo);
  }
}
