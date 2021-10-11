public interface IBoardMenu
{
    BoardMenuID menuID { get; }
    event System.Action<BoardMenuID, object> OnRequestingOpenMenu;

    void Initialize(params object[] arguments);
    void Show();
    void Hide();
}
