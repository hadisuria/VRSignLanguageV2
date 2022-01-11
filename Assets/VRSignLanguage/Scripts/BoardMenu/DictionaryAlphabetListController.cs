using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryAlphabetListController : MonoBehaviour
{
    [SerializeField] private AlphabetDetailHandler alphabetDetailHandler;
    [SerializeField]
    private TextMeshProUGUI activeAlphabetText;


    void Start()
    {
        foreach (int i in Enum.GetValues(typeof(AlphabetID)))
        {

            AlphabetButton temp = Instantiate(Resources.Load<GameObject>("Menu/AlphabetButton"), transform).GetComponent<AlphabetButton>();
            temp.gameObject.SetActive(true);
            temp.OnWordClicked(() =>
            {
                alphabetDetailHandler.InitializeImage((AlphabetID)i);
                activeAlphabetText.SetText(((AlphabetID)i).ToString());
            }
            );

            temp.Init(((AlphabetID)i).ToString());
        }
    }

}
