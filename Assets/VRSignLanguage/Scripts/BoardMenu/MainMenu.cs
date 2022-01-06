using System;
using UnityEngine;

public class MainMenu : MonoBehaviour, IMainMenu
{
    #region IMainMenu
    public MenuID menuID { get; } = MenuID.MainMenu;
    public event Action<MenuID, object> OnRequestingOpenMenu;
    #endregion

    private bool initalized = false;
    // [SerializeField] ButtonEvent dictionaryButton;
    // [SerializeField] ButtonEvent learnButton;
    // [SerializeField] ButtonEvent inputWordButton;
    [SerializeField] ButtonEvent exitButton;

    public void Initialize(params object[] arguments)
    {
        if (!initalized)
        {
            // dictionaryButton.OnButtonClicked += DictionaryButton_OnButtonHit; ;
            // learnButton.OnButtonClicked += LearnButton_OnButtonHit; ;
            // inputWordButton.OnButtonClicked += InputWordButton_OnButtonHit; ;
            exitButton.OnButtonClicked += ExitGame;
            initalized = true;
#if !UNITY_EDITOR
            inputWordButton.gameObject.SetActive(false);
#endif
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }



    private void ExitGame()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        // dictionaryButton.OnButtonClicked -= DictionaryButton_OnButtonHit;
        // learnButton.OnButtonClicked -= LearnButton_OnButtonHit;
        // inputWordButton.OnButtonClicked -= InputWordButton_OnButtonHit;
        // exitButton.OnButtonClicked -= ExitGame;

    }
}
