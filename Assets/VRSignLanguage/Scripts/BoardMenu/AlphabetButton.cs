using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// [RequireComponent(typeof(ButtonEvent))]
public class AlphabetButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myString;
    private MenuManager menuController;
    private Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    public void Init(String alphabet)
    {
        SetText(alphabet);
    }

    private void SetText(string textString)
    {
        myString.text = textString;
    }

    public void OnWordClicked(UnityAction doThis)
    {
        myButton.onClick.AddListener(doThis);
        //wordListControl.ButtonClick(myTextString);
        // menuController.OpenMenu(BoardMenuID.SignLanguagePreview, myGuideBall);
    }
}