using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DictionaryAlphabetListController : MonoBehaviour
{
    [SerializeField] private AlphabetDetailHandler alphabetDetailHandler;

    void Start()
    {
        foreach (int i in Enum.GetValues(typeof(AlphabetID)))
        {

            AlphabetButton temp = Instantiate(Resources.Load<GameObject>("Menu/AlphabetButton"), transform).GetComponent<AlphabetButton>();
            temp.gameObject.SetActive(true);
            temp.OnWordClicked(() => alphabetDetailHandler.InitializeImage((AlphabetID)i));

            temp.Init(((AlphabetID)i).ToString());

            Debug.Log(((AlphabetID)i).ToString());

            // buttons.Add(temp);
        }
    }

}
