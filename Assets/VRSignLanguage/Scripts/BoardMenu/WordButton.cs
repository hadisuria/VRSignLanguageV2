using TMPro;
using UnityEngine;

[RequireComponent(typeof(ButtonEvent))]
public class WordButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myString;
    private ButtonEvent buttonEvent;
    private GuideBall myGuideBall;
    //private string myTextString;
    private BoardMenuController menuController;

    private void Awake() {
        buttonEvent = GetComponent<ButtonEvent>();
        buttonEvent.OnButtonClicked += OnWordClicked;
        menuController = FindObjectOfType<BoardMenuController>();
    }

    public void Init(GuideBall value)
    {
        SetText(value.word);
        myGuideBall = value;
    }

    private void SetText(string textString){
        //myTextString = textString;
        myString.text = textString;
    }

    private void OnWordClicked(){
        //wordListControl.ButtonClick(myTextString);
        menuController.OpenMenu(BoardMenuID.SignLanguagePreview, myGuideBall);
    }
}
