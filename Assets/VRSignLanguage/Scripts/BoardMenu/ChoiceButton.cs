using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ButtonEvent))]
public class ChoiceButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myTextString;
    private ButtonEvent buttonEvent;
    private string myString;

    public event Action<string> OnClickingButton;

    private void Awake()
    {
        buttonEvent = GetComponent<ButtonEvent>();
		buttonEvent.OnButtonClicked += ButtonEvent_OnButtonClicked;
    }

	private void ButtonEvent_OnButtonClicked()
	{
        OnClickingButton?.Invoke(myString);
	}

    public void SetText(string value)
    {
        myString = value;
        myTextString.text = value;
    }
}
