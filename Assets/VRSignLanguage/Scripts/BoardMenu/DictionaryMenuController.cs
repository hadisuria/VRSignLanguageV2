using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
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

    [SerializeField]
    private Image targetColorBackground;

    [SerializeField]
    private Color correctColor;
    [SerializeField]
    private Color falseColor;


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
        GameState.SetCurrState(GameState.state.Dictionary);
    }

    private void CloseMenu()
    {
        alphabetDetailHandler.gameObject.SetActive(false);
        GameState.SetCurrState(GameState.state.Stop);
        MenuManager.OpenMenu_Static(MenuID.Previous);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (GameState.isGestureCorrect)
        {
            targetColorBackground.color = correctColor;
            GameState.SetIsGestureCorrect(false);
        }
        else
        {
            targetColorBackground.color = falseColor;
        }

    }

}
