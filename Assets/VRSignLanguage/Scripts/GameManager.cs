using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPoolHandler;
    [SerializeField] private VRInputHandler inputHandler;
    [SerializeField] private MenuManager menuController;

    private bool prevInputHandlerPrimaryButtonLeft = false;
    private bool prevInputHandlerPrimaryButtonRight = false;
    public static bool isRayActive { get; private set; }
    [SerializeField] private VRInputModule vrInputModule;



    private void Awake()
    {

    }

    private void Update()
    {
        HandleRayActive();
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     enemyPoolHandler.SummonEnemy();
        // }
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     enemyPoolHandler.DestroyEnemy();
        // }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ScreenshotHandler.TakeScreenshot_Static(1920, 1080);
        }
    }

    private void HandleRayActive()
    {
        bool rightPrimaryButton = inputHandler.GetRightHandController().primaryButton;
        bool leftPrimaryButton = inputHandler.GetLeftHandController().primaryButton;


        if (vrInputModule.mainController == OVRInput.Controller.RTouch)
        {
            if (rightPrimaryButton)
            {
                if (!prevInputHandlerPrimaryButtonRight)
                {
                    prevInputHandlerPrimaryButtonRight = true; ;
                    isRayActive = !isRayActive;
                }
            }
            else
            {
                prevInputHandlerPrimaryButtonRight = false;
            }
        }
        else if (vrInputModule.mainController == OVRInput.Controller.LTouch)
        {
            if (leftPrimaryButton)
            {
                if (!prevInputHandlerPrimaryButtonLeft)
                {
                    prevInputHandlerPrimaryButtonLeft = true;
                    isRayActive = !isRayActive;
                }
            }
            else
            {
                prevInputHandlerPrimaryButtonLeft = false;



            }
        }

    }


}
