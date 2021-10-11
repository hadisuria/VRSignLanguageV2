using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private VRInputHandler inputHandler;
    [SerializeField] private BoardMenuController menuController;

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
