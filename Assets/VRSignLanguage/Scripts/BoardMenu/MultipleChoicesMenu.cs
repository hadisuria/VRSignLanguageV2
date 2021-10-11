using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class MultipleChoicesMenu : MonoBehaviour, IBoardMenu
{
	#region IBoardMenu
	public BoardMenuID menuID { get; } = BoardMenuID.MultipleChoices;

	public event Action<BoardMenuID, object> OnRequestingOpenMenu;
	#endregion

	[SerializeField] private ButtonEvent backButton;
	[SerializeField] private ButtonEvent nextButton;
	[SerializeField] private TextMeshProUGUI answerText;
	[SerializeField] private List<ChoiceButton> choicesButtons = new List<ChoiceButton>();
	[SerializeField] private VideoPlayer videoPlayer;
	[SerializeField] private SignLanguagePreviewMenu previewMenu;

	private bool isInitalized = false;
	private List<GuideBall> currSection;
	private int currQuestionIndex = 0;

	public void Hide()
	{
		gameObject.SetActive(false);
		currQuestionIndex = 0;
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Initialize(params object[] arguments)
	{
		if (!isInitalized)
		{
			backButton.OnButtonClicked += BackButton_OnButtonClicked;
			nextButton.OnButtonClicked += NextButton_OnButtonClicked;
			for (int i = 0; i < choicesButtons.Count; i++)
			{
				choicesButtons[i].OnClickingButton += CheckingAnswer;
			}
			isInitalized = true;
		}

		currSection = new List<GuideBall>( (List<GuideBall>) arguments[0]);

		SectionRandomizer();
		PrepareQuestion(currQuestionIndex);
	}


	private void CheckingAnswer(string answer)
	{
		foreach(ChoiceButton b in choicesButtons)
		{
			b.gameObject.SetActive(false);
		}

		if(answer == currSection[currQuestionIndex].word)
		{
			// correct answer
			answerText.text = "Your Answer is Correct!!! \n\n Correct Answer :\n" + currSection[currQuestionIndex].word;
		}
		else
		{
			// wrong answer
			answerText.text = "Your Answer is Incorrect!!! \n\n Correct Answer :\n" + currSection[currQuestionIndex].word;
		}
		answerText.gameObject.SetActive(true);
		nextButton.gameObject.SetActive(true);

		previewMenu.Show();
		previewMenu.Initialize(currSection[currQuestionIndex]);
	}

	private void PrepareQuestion(int index)
	{
		// prepare video
		try
		{
			videoPlayer.clip = Resources.Load<VideoClip>("Video/" + currSection[index].word);
		}
		catch(Exception e)
		{
			Debug.Log("Video Clip Error Message : " + e);
		}
		videoPlayer.isLooping = true;
		videoPlayer.Play();

		// prepare choices buttons
		List<GuideBall> tempSection = new List<GuideBall>(currSection);
		List<string> tempString = new List<string>();
		tempString.Add(tempSection[index].word);
		tempSection.RemoveAt(index);

		int randomIndex;
		for(int i = 1; i < choicesButtons.Count; i++)
		{	
			// find random index that doesn't exist in tempString
			do{
				randomIndex = UnityEngine.Random.Range(0, tempSection.Count);
			} while(tempString.Exists(e => e.Equals(tempSection[randomIndex].word)));
			
			// Add choosen random string to temporary string list
			tempString.Add(tempSection[randomIndex].word);
			// remove choosen list index from the temporary section list to prevent same choice later
			tempSection.RemoveAt(randomIndex);
		}
		for (int i = 0; i < choicesButtons.Count; i++)
		{
			randomIndex = UnityEngine.Random.Range(0, tempString.Count);
			choicesButtons[i].SetText(tempString[randomIndex]);
			tempString.RemoveAt(randomIndex);

			choicesButtons[i].gameObject.SetActive(true);
		}

		// arrange button
		nextButton.gameObject.SetActive(false);
		answerText.gameObject.SetActive(false);
		previewMenu.Hide();
	}

	private void SectionRandomizer()
	{
		List<GuideBall> temp = new List<GuideBall>();
		for(int i = currSection.Count - 1; i >= 0; i--)
		{
			int randomIndex = UnityEngine.Random.Range(0, currSection.Count);
			GuideBall tempData = new GuideBall(currSection[randomIndex].word, currSection[randomIndex].leftHandPath, currSection[randomIndex].rightHandPath);
			temp.Add(tempData);
			currSection.RemoveAt(randomIndex);
		}
		currSection.Clear();
		currSection = AddSpacedRepetition(temp);
		Debug.Log( "Data count after random : " + currSection.Count);
	}


	// function to add spaced repetition wannabe
	private List<GuideBall> AddSpacedRepetition(List<GuideBall> currSectionQuestions) {
		List<GuideBall> temp = new List<GuideBall>();
		var rememberIndex = 0; // index to show current remember question index
		var interval = 3; // interval show before question index

		for(int i=0; i<currSectionQuestions.Count; i++){
			// every interval add remember question into section question
			if((i+1) > interval){
				temp.Add(currSectionQuestions[rememberIndex]);
				rememberIndex++;
			}
			// add every question to section question
			temp.Add(currSectionQuestions[i]);
		}
		
		return temp;
	}

	private void NextButton_OnButtonClicked()
	{
		currQuestionIndex++;
		if(currQuestionIndex == currSection.Count)
		{
			// finish section
			OnRequestingOpenMenu?.Invoke(BoardMenuID.LearnMenu, null);
		}
		else
		{
			PrepareQuestion(currQuestionIndex);
		}
	}

	private void BackButton_OnButtonClicked()
	{
		OnRequestingOpenMenu?.Invoke(BoardMenuID.LearnMenu, null);
	}
}
