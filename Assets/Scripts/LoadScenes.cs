using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour
{
  public static void loadMainMenuScene() {
    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
  }

  public void quitGame() {
    Application.Quit();
  }
}