using System;
using UnityEngine;

public class DictionaryMenu : MonoBehaviour, IBoardMenu
{
	#region IBoardMenu
	public BoardMenuID menuID { get; } = BoardMenuID.DictionaryMenu;

	public event Action<BoardMenuID, object> OnRequestingOpenMenu;
	#endregion

	private bool initialized = false;
	[SerializeField] private ButtonEvent backButton;
	[SerializeField] private WordListControl wordListControl;

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	public void Initialize(params object[] arguments)
	{
		if (!initialized)
		{
			backButton.OnButtonClicked += BackButton_OnButtonHit;
			initialized = true;
		}
		// Generate Everytime Menu Open (Testing purpose only, should only generate at start on production)
		wordListControl.GenerateWordList();
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void BackButton_OnButtonHit()
	{
		OnRequestingOpenMenu?.Invoke(BoardMenuID.MainMenu, null);
	}

	private void OnDestroy()
	{
		backButton.OnButtonClicked -= BackButton_OnButtonHit;
	}
}
