using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryMenuController : MonoBehaviour, IMainMenu
{
    public MenuID menuID { get; } = MenuID.Dictionary;
    public bool initialized { get; set; } = false;

    //public event Action<MenuID, object> OnRequestingOpenMenu;

    [SerializeField]
    private Button backButton;

    [SerializeField]
    private AlphabetDetailHandler alphabetDetailHandler;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(params object[] arguments)
    {
        if (!initialized)
        {
            backButton.onClick.AddListener(CloseMenu);
            initialized = true;
        }
    }

    private void CloseMenu()
    {
        alphabetDetailHandler.gameObject.SetActive(false);
        MenuManager.OpenMenu_Static(MenuID.Previous);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }


}
