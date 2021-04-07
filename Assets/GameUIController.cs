using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIController : MonoBehaviour
{

    [SerializeField] private GameHUDWidget GameCanvas;
    [SerializeField] private GameHUDWidget PauseCanvas;

    private GameHUDWidget ActiveWidget;

    private void Start()
    {
        DisableAllMenus();
        EnableGameMenu();
    }

    public void EnablePauseMenu()
    {
        if (ActiveWidget) ActiveWidget.DisableWidget();

        ActiveWidget = PauseCanvas;
        ActiveWidget.EnableWidget();
    }
    public void EnableGameMenu()
    {
        if (ActiveWidget) ActiveWidget.DisableWidget();

        ActiveWidget = GameCanvas;
        ActiveWidget.EnableWidget();
    }

    public void DisableAllMenus()
    {
        GameCanvas.DisableWidget();
        PauseCanvas.DisableWidget();
    }
}