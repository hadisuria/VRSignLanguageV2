using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour, IMainMenu
{
    public MenuID menuID { get; } = MenuID.GameOver;

    public bool initialized { get; set; } = false;

    [SerializeField]
    private Button beginnerButton;
    [SerializeField]
    private Button memorizedButton;
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
            beginnerButton.onClick.AddListener(() => { PlayAgain(GameState.state.Beginner); });
            memorizedButton.onClick.AddListener(() => { PlayAgain(GameState.state.Memorized); });
            mainMenuButton.onClick.AddListener(OpenMainMenu);
            initialized = true;
        }
        scoreText.SetText(((int)arguments[0]).ToString());
    }

    private void OpenMainMenu()
    {
        MenuManager.OpenMenu_Static(MenuID.MainMenu);
    }

    private void PlayAgain(GameState.state gameMode)
    {
        MenuManager.OpenMenu_Static(MenuID.None);
        GameState.SetCurrState(gameMode);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
}
