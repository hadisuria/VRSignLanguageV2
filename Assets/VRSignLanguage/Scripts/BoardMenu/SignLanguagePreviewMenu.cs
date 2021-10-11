using System;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class SignLanguagePreviewMenu : MonoBehaviour, IBoardMenu
{
	#region IBoardMenu
	public BoardMenuID menuID { get; } = BoardMenuID.SignLanguagePreview;

	public event Action<BoardMenuID, object> OnRequestingOpenMenu;
	#endregion

	private bool initialized = false;
	[SerializeField] private VideoPlayer videoPlayer;
	[SerializeField] private TextMeshProUGUI wordText;
	[SerializeField] private ButtonEvent backButton;
	[SerializeField] private GuideBallPathSpawner guideBallPathSpawner;

	private GuideBall targetGuideBall;

	public void Hide()
	{
		guideBallPathSpawner.ResetData();
		gameObject.SetActive(false);
	}

	public void Initialize(params object[] arguments)
	{
		targetGuideBall = (GuideBall) arguments[0];
		guideBallPathSpawner.SetGuideBallData(targetGuideBall);
		guideBallPathSpawner.InitBall();

		wordText.text = targetGuideBall.word;

		SetVideo();

		if (!initialized)
		{
			backButton.OnButtonClicked += BackButton_OnButtonClicked;
			initialized = true;
		}
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	private void BackButton_OnButtonClicked()
	{
		OnRequestingOpenMenu?.Invoke(BoardMenuID.Previous, null);
	}

	private void SetVideo()
	{
		try
		{
			videoPlayer.clip = Resources.Load<VideoClip>("Video/" + targetGuideBall.word);

			videoPlayer.Play();
		}
		catch (Exception e)
		{
			Debug.Log(e);
		}
	}
}
