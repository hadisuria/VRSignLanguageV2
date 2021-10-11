using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InputWordMenu : MonoBehaviour, IBoardMenu
{
	#region IBoardMenu
	public BoardMenuID menuID { get; } = BoardMenuID.InputWordMenu;

	public event Action<BoardMenuID, object> OnRequestingOpenMenu;
	#endregion

	private bool initialized = false;
	[SerializeField] private ButtonEvent resetButton;
	[SerializeField] private ButtonEvent inputWordButton;
	[SerializeField] private ButtonEvent backButton;
	[SerializeField] private TMP_InputField typedWord;

	[SerializeField] private GuideBallHandler ballHandler;
	//private SignLanguageDictionary languageDictionary = new SignLanguageDictionary();
	private GameManager gameManager;

	public void Hide()
	{
		gameObject.SetActive(false);
		ballHandler.ResetList();
		typedWord.text = "";
	}

	public void Initialize(params object[] arguments)
	{
		if (!initialized)
		{
			resetButton.OnButtonClicked += ResetButton_OnButtonHit;
			inputWordButton.OnButtonClicked += InputWordButton_OnButtonHit;
			backButton.OnButtonClicked += BackButton_OnButtonHit;
			gameManager = FindObjectOfType<GameManager>();
			initialized = true;
		}
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void BackButton_OnButtonHit()
	{
		ballHandler.ResetList();
		typedWord.text = "";
		OnRequestingOpenMenu?.Invoke(BoardMenuID.Previous, null);
	}

	private void InputWordButton_OnButtonHit()
	{
		if(typedWord.text != "" && typedWord.text != " ")
		{
			(List<Vector3> leftTemp, List<Vector3> rightTemp) = ballHandler.CalculateOffset();
			GuideBall tempBall = new GuideBall(typedWord.text, leftTemp, rightTemp);
			gameManager.languageDictionary.AddWord(tempBall);
			ballHandler.ResetList();
			Debug.Log($"Guide Ball Data Saved With \"{typedWord.text}\"") ;
			typedWord.text = "";
		} else
		{
			Debug.Log("Failed to saved guideball data, please input word");
		}
	}

	private void ResetButton_OnButtonHit()
	{
		ballHandler.ResetList();
		typedWord.text = "";
	}

	private void OnDestroy()
	{
		resetButton.OnButtonClicked -= ResetButton_OnButtonHit;
		inputWordButton.OnButtonClicked -= InputWordButton_OnButtonHit;
		backButton.OnButtonClicked -= BackButton_OnButtonHit;
	}
}
