using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ButtonEvent))]
public class AlphabetButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myString;
    private ButtonEvent buttonEvent;
    private BoardMenuController menuController;

    // private void Awake()
    // {
    //     buttonEvent = GetComponent<ButtonEvent>();
    //     buttonEvent.OnButtonClicked += OnWordClicked;
    //     menuController = FindObjectOfType<BoardMenuController>();
    // }

    public void Init(String alphabet)
    {
        SetText(alphabet);
    }

    private void SetText(string textString)
    {
        myString.text = textString;
    }

    private void OnWordClicked()
    {
        //wordListControl.ButtonClick(myTextString);
        // menuController.OpenMenu(BoardMenuID.SignLanguagePreview, myGuideBall);
    }
}