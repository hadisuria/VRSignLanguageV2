using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour, IMainMenu
{
    #region IMainMenu
    public MenuID menuID { get; } = MenuID.MainMenu;

    public bool initialized { get; set; } = false;

    //public event Action<MenuID, object> OnRequestingOpenMenu;
    #endregion

    [SerializeField]
    private Button beginnerButton;
    [SerializeField]
    private Button memorizedButton;
    [SerializeField]
    private Button dictionaryButton;

    [SerializeField]
    private Button exitButton;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(params object[] arguments)
    {
        if (!initialized)
        {
            beginnerButton.onClick.AddListener(() => { StartGame(GameState.state.Beginner); });
            memorizedButton.onClick.AddListener(() => { StartGame(GameState.state.Memorized); });
            dictionaryButton.onClick.AddListener(OpenDictionary);
            exitButton.onClick.AddListener(ExitGame);
            initialized = true;
        }
    }

    private void StartGame(GameState.state gameMode)
    {
        MenuManager.OpenMenu_Static(MenuID.None);
        GameState.SetCurrState(gameMode);
    }

    private void ExitGame()
    {
        MenuManager.OpenMenu_Static(MenuID.Exit);
    }

    private void OpenDictionary()
    {
        MenuManager.OpenMenu_Static(MenuID.Dictionary);
        Debug.Log("opening menu dictionary");
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}
