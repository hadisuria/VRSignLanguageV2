using System;
using UnityEngine;

public class LearnMenu : MonoBehaviour, IBoardMenu
{
	#region IBoardMenu
	public BoardMenuID menuID { get; } = BoardMenuID.LearnMenu;

	public event Action<BoardMenuID, object> OnRequestingOpenMenu;
	#endregion

	private bool initialized = false;
	[SerializeField] private ButtonEvent backButton;
	[SerializeField] private WordSectionControl sectionControl;

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
		// Generate Section Everytime Menu Open (Testing purpose only, should only generaate at start on productiona)
		sectionControl.GenerateSection();
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