using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DictionaryAlphabetListController : MonoBehaviour
{
    void Start()
    {
        foreach (int i in Enum.GetValues(typeof(AlphabetID)))
        {

            AlphabetButton temp = Instantiate(Resources.Load<GameObject>("Menu/AlphabetButton"), transform).GetComponent<AlphabetButton>();
            temp.gameObject.SetActive(true);

            temp.Init(((AlphabetID)i).ToString());

            Debug.Log(((AlphabetID)i).ToString());

            // buttons.Add(temp);
        }
    }

}
