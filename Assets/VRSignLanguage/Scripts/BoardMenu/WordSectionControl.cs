using System.Collections.Generic;
using UnityEngine;


public class WordSectionControl : MonoBehaviour
{
    private List<WordSectionButton> buttons = new List<WordSectionButton>();
    [SerializeField] private int dataPerSection = 5;

    public static List<List<GuideBall>> wordSections = new List<List<GuideBall>>();
    private GameManager gameManager;

    private void Awake(){
        gameManager = FindObjectOfType<GameManager>();
    }

    public void GenerateSection()
    {
        ResetData();
        // function to generate section of question / words
        List<GuideBall> guideBallData = new List<GuideBall>();
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();

        guideBallData = gameManager.languageDictionary.guideBallDataList;

        List<GuideBall> tempGuideBallList = new List<GuideBall>();

        int currSection = 0;
        for (int i = 0; i < guideBallData.Count; i++)
        {
            tempGuideBallList.Add(guideBallData[i]);
            if ((i + 1) % dataPerSection == 0)
            {
                wordSections.Add(new List<GuideBall>(tempGuideBallList));
                WordSectionButton temp = Instantiate(Resources.Load<GameObject>("WordSectionButton"), transform).GetComponent<WordSectionButton>();
                temp.gameObject.SetActive(true);
                temp.Init(wordSections[currSection], $"Section {currSection}");
                buttons.Add(temp);
                tempGuideBallList.Clear();
                currSection++;
            }
        }
    }

    private void ResetData()
	{
        if (buttons.Count > 0)
        {
            foreach (WordSectionButton button in buttons)
            {
                Destroy(button.gameObject);
            }
            buttons.Clear();
        }
    }
}
