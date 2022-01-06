public interface IMainMenu 
{
    MenuID menuID { get; }
    event System.Action<MenuID, object> OnRequestingOpenMenu;

    void Initialize(params object[] arguments);
    void Show();
    void Hide();
}
