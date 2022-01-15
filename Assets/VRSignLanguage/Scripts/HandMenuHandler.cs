using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenuHandler : MonoBehaviour
{
    private bool isMenuActive = false;
    [SerializeField] private GameObject MainMenu;

    private void SetMenuActive()
    {
        isMenuActive = !isMenuActive;
        MainMenu.SetActive(isMenuActive);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "RightHand")
            SetMenuActive();
    }
}
