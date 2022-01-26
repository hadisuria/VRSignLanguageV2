using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditMenuController : MonoBehaviour, IMainMenu
{
    public MenuID menuID { get; } = MenuID.Credit;

    public bool initialized { get; set; } = false;

    [SerializeField]
    private Button backButton;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Initialize(params object[] arguments)
    {
        if (!initialized)
        {
            backButton.onClick.AddListener(CloseMenu);
            initialized = true;
        }
    }

    private void CloseMenu()
    {
        MenuManager.OpenMenu_Static(MenuID.Previous);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
