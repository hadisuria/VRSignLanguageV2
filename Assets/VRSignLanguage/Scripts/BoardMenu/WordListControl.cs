using System.Collections.Generic;
using UnityEngine;



public class WordListControl : MonoBehaviour
{
    private List<WordButton> buttons = new List<WordButton>();
    [SerializeField]
    private GameManager gameManager;

    public void GenerateWordList(){

        //test debug
        ResetData();
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        for (int i = 0; i < gameManager.languageDictionary.guideBallDataList.Count; i++) 
		{
            WordButton temp = Instantiate(Resources.Load<GameObject>("WordButton"), transform).GetComponent<WordButton>();
            temp.gameObject.SetActive(true);
            // if(temp.TryGetComponent<ButtonEvent>(out ButtonEvent target))
            // {
            //     target.OnButtonClicked += ShowWordDetail();
            // }
            // temp.GetComponentInChildren<TextMeshProUGUI>().text =  (i+1) + ". " + gameManager.languageDictionary.guideBallDataList[i].word;
			// temp.transform.SetParent (transform, false);
            temp.Init(gameManager.languageDictionary.guideBallDataList[i]);

            buttons.Add(temp);
		}

    }

    private void ResetData(){
        if(buttons.Count > 0 ){
            foreach(WordButton button in buttons){
                Destroy(button.gameObject); 
            }
            buttons.Clear();
        }
    }

    void Awake(){
        gameManager = FindObjectOfType<GameManager>();
        //GenerateWordList();
    }
}
