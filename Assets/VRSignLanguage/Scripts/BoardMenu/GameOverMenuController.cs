using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour, IMainMenu
{
    public MenuID menuID { get; } = MenuID.GameOver;

    public bool initialized { get; set; } = false;

    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private TextMeshProUGUI scoreText;

    public void Hide()
    {
        gameObject.SetActive(false);
    }


    public void Initialize(params object[] arguments)
    {
        if (!initialized)
        {
            playButton.onClick.AddListener(PlayAgain);
            mainMenuButton.onClick.AddListener(OpenMainMenu);
            initialized = true;
        }
        scoreText.SetText(((int)arguments[0]).ToString());
    }

    private void OpenMainMenu()
    {
        MenuManager.OpenMenu_Static(MenuID.MainMenu);
    }

    private void PlayAgain()
    {
        MenuManager.OpenMenu_Static(MenuID.None);
        GameState.SetCurrState(GameState.state.Play);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
