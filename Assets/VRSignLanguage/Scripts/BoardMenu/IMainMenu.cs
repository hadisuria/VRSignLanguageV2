public interface IMainMenu
{
    MenuID menuID { get; }
    //event System.Action<MenuID, object> OnRequestingOpenMenu;

    bool initialized { get; set; }
    void Initialize(params object[] arguments);
    void Show();
    void Hide();
}
