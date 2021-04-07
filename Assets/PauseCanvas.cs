using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseCanvas : GameHUDWidget
{

    public void ResumeGame()
    {
        PauseManager.Instance.UnPauseGame();
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
