using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPoolHandler;
    // [SerializeField] private VRInputHandler inputHandler;

    // private bool prevInputHandlerPrimaryButtonLeft = false;
    // private bool prevInputHandlerPrimaryButtonRight = false;
    public static bool isRayActive { get; private set; }
    // [SerializeField] private VRInputModule vrInputModule;

    [SerializeField] private ScreenshotHandler ss1;
    [SerializeField] private ScreenshotHandler ss2;

    [SerializeField] private float summonEnemyDelay;
    private float delayCounter = 0f;

    private int score;
    [SerializeField] private int lives;
    private int liveCounter;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private LineRenderer laserPointer;

    private void Start()
    {
        ResetData();
        MenuManager.OpenMenu_Static(MenuID.MainMenu);
    }

    private void Update()
    {
        //HandleRayActive();
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     enemyPoolHandler.SummonEnemy();
        // }
        // if (Input.GetKeyDown(KeyCode.Tab))
        // {
        //     enemyPoolHandler.DestroyEnemy();
        // }

        // // take screenshot
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     ss1.TakeScreenshot(1920, 1080);
        //     ss2.TakeScreenshot(1920, 1080);
        // }

        if (GameState.currState == GameState.state.Play)
        {
            SummoningEnemy();
            laserPointer.gameObject.SetActive(false);
            ToggleScoreBoard(true);
            scoreText.SetText(score.ToString());
        }

        if (GameState.isEnemyHitPlayer)
        {
            enemyPoolHandler.DestroyEnemy(enemyPoolHandler.usedEnemies[0]);
            GameState.SetIsEnemyHitPlayer(false);
            liveCounter--;
            if (liveCounter <= 0)
            {
                GameState.SetCurrState(GameState.state.Stop);
                ToggleScoreBoard(false);
                laserPointer.gameObject.SetActive(true);
                // open game over menu n reset score
                MenuManager.OpenMenu_Static(MenuID.GameOver, score);
                ResetData();
            }
        }

        if (GameState.isGestureCorrect)
        {
            score += 100;
            GameState.SetIsGestureCorrect(false);

            //Update score UI
            //update text di sini
            scoreText.SetText(score.ToString());
        }
    }

    private void ResetData()
    {
        score = 0;
        liveCounter = lives;
    }

    private void ToggleScoreBoard(bool value)
    {
        scoreText.transform.parent.parent.gameObject.SetActive(value);
    }

    private void SummoningEnemy()
    {
        if (delayCounter <= 0)
        {
            enemyPoolHandler.SummonEnemy();
            delayCounter = summonEnemyDelay;
        }
        delayCounter -= Time.deltaTime;
    }

    // private void HandleRayActive()
    // {
    //     bool rightPrimaryButton = inputHandler.GetRightHandController().primaryButton;
    //     bool leftPrimaryButton = inputHandler.GetLeftHandController().primaryButton;


    //     if (vrInputModule.mainController == OVRInput.Controller.RTouch)
    //     {
    //         if (rightPrimaryButton)
    //         {
    //             if (!prevInputHandlerPrimaryButtonRight)
    //             {
    //                 prevInputHandlerPrimaryButtonRight = true; ;
    //                 isRayActive = !isRayActive;
    //             }
    //         }
    //         else
    //         {
    //             prevInputHandlerPrimaryButtonRight = false;
    //         }
    //     }
    //     else if (vrInputModule.mainController == OVRInput.Controller.LTouch)
    //     {
    //         if (leftPrimaryButton)
    //         {
    //             if (!prevInputHandlerPrimaryButtonLeft)
    //             {
    //                 prevInputHandlerPrimaryButtonLeft = true;
    //                 isRayActive = !isRayActive;
    //             }
    //         }
    //         else
    //         {
    //             prevInputHandlerPrimaryButtonLeft = false;
    //         }
    //     }
    // }
}
