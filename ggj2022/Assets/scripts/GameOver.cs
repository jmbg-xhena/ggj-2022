using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
  public void Restartbutton()
    {
        SceneManager.LoadScene("Juego");
    }

    public void MainMenubutton()
    {
        SceneManager.LoadScene("Menu");
    }
}
