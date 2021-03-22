using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWidget : MenuWidget
{
   public void OpenStartMenu()
   {
       MenuController.EnableMenu("LoadGameMenu");
   }

    public void OpenOptionsMenu()
    {
        MenuController.EnableMenu("OptionsMenu");
    }

    public void QuitApp()
    {
        Application.Quit();
    }
}
