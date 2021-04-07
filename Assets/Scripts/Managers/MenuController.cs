using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string StartingMenu = "MainMenu";
    [SerializeField] private string RootMenu = "MainMenu";
    private MenuWidget ActiveWidget;
    private Dictionary<string, MenuWidget> Menus = new Dictionary<string, MenuWidget>();
    // Start is called before the first frame update
    void Start()
    {
        AppEvents.Invoke_OnMousecursorEnable(true);
        DisableAllMenus();
        EnableMenu("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void AddMenu(string menuName, MenuWidget menuWidget)
    {
        if (string.IsNullOrEmpty(menuName)) return;
        if(Menus.ContainsKey(menuName))
        {
            print("Menu exists");
            return;
        }

        if (menuWidget == null) return;
        Menus.Add(menuName, menuWidget);
    }

    public void EnableMenu(string menuName)
    {
        if (string.IsNullOrEmpty(menuName)) return;
        if (Menus.ContainsKey(menuName))
        {
            DisableActiveMenu();
            ActiveWidget = Menus[menuName];
            ActiveWidget.EnableWidget();
        }
    }
    public void DisableMenu(string menuName)
    {
        if (string.IsNullOrEmpty(menuName)) return;
        if (Menus.ContainsKey(menuName))
        {
            Menus[menuName].DisableWidget();
        }
    }

    public void ReturnToRootMenu()
    {
        DisableAllMenus();
        EnableMenu("MainMenu");
    }
    private void DisableActiveMenu()
    {
        if(ActiveWidget)
        {
            ActiveWidget.DisableWidget();

        }
    }

    private void DisableAllMenus()
    {
        foreach (MenuWidget item in Menus.Values)
        {
            item.DisableWidget();
        }
    }
}

public abstract class MenuWidget : MonoBehaviour
{
    [SerializeField] private string MenuName; // string key
    protected MenuController MenuController;

    private void Awake()
    {
        MenuController = FindObjectOfType<MenuController>();
        if (MenuController)
        {
            MenuController.AddMenu(MenuName, this);
        }
        else
            print("Menu controller not found");
    }

    public void EnableWidget()
    {
        gameObject.SetActive(true);
    }

    public void DisableWidget()
    {
        gameObject.SetActive(false);
    }

    public void ReturntoRoot()
    {
        if(MenuController)
        {
            MenuController.ReturnToRootMenu();
        }
    }
}