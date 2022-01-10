using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitMenuController : MonoBehaviour, IMainMenu
{
    public MenuID menuID { get; } = MenuID.Exit;
    public bool initialized { get; set; } = false;


    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private Button backButton;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(params object[] arguments)
    {
        if (!initialized)
        {
            quitButton.onClick.AddListener(ExitGame);
            backButton.onClick.AddListener(CloseMenu);
            initialized = true;
        }
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void CloseMenu()
    {
        MenuManager.OpenMenu_Static(MenuID.Previous);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


}
