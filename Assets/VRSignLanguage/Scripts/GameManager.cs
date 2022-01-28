using Oculus.Platform;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyPool enemyPoolHandler;
    // [SerializeField] private VRInputHandler inputHandler;

    // private bool prevInputHandlerPrimaryButtonLeft = false;
    // private bool prevInputHandlerPrimaryButtonRight = false;
    //public static bool isRayActive { get; private set; }
    // [SerializeField] private VRInputModule vrInputModule;

    [SerializeField] private bool EnableScreenshot = false;
    [SerializeField] private ScreenshotHandler ss1;
    [SerializeField] private ScreenshotHandler ss2;

    [SerializeField] private float summonEnemyDelay;
    private float delayCounter = 0f;
    private float timeSummonFaster;
    private float timeMultiplierCount = 0f;

    private int score;
    [SerializeField] private int lives;
    private int liveCounter;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private LineRenderer laserPointer;

    [SerializeField] private AudioSource gestureCorrectSound;

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

        // take screenshot
        if (EnableScreenshot)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ss1.TakeScreenshot(1920, 1080);
                ss2.TakeScreenshot(1920, 1080);
            }
        }
        if (GameState.currState == GameState.state.Beginner || GameState.currState == GameState.state.Memorized)
        {
            if (GameState.currState == GameState.state.Memorized)
            {
                SummoningEnemy(false);
            }
            else if (GameState.currState == GameState.state.Beginner)
            {
                SummoningEnemy(true);
            }

            laserPointer.gameObject.SetActive(false);
            ToggleScoreBoard(true);
            scoreText.SetText(score.ToString());
            healthText.SetText(liveCounter.ToString());

            if (GameState.isGestureCorrect)
            {
                score += 100;
                GameState.SetIsGestureCorrect(false);
                gestureCorrectSound.Play();

                //Update score UI
                //update text di sini
                scoreText.SetText(score.ToString());
            }


            if (GameState.isEnemyHitPlayer)
            {
                enemyPoolHandler.DestroyEnemy(enemyPoolHandler.usedEnemies[0]);
                GameState.SetIsEnemyHitPlayer(false);
                liveCounter--;
                healthText.SetText(liveCounter.ToString());
                if (liveCounter <= 0)
                {
                    GameState.SetCurrState(GameState.state.Stop);
                    while (enemyPoolHandler.usedEnemies.Count > 0)
                    {
                        enemyPoolHandler.DestroyEnemy(enemyPoolHandler.usedEnemies[0]);
                    }
                    ToggleScoreBoard(false);
                    laserPointer.gameObject.SetActive(true);
                    // check n set highscore
                    if (PlayerPrefs.GetInt($"{GameState.prevState.ToString()}_Highscore", 0) < score)
                    {
                        PlayerPrefs.SetInt($"{GameState.prevState.ToString()}_Highscore", score);
                    }
                    // open game over menu n reset score
                    MenuManager.OpenMenu_Static(MenuID.GameOver, score);
                    ResetData();
                }
            }
        }
    }

    private void ResetData()
    {
        score = 0;
        liveCounter = lives;
        timeSummonFaster = summonEnemyDelay;
        delayCounter = 0f;
        timeMultiplierCount = 5f;
    }

    private void ToggleScoreBoard(bool value)
    {
        scoreText.transform.parent.parent.gameObject.SetActive(value);
    }

    private void SummoningEnemy(bool isBeginnerMode)
    {
        timeMultiplierCount -= Time.deltaTime;
        if (timeMultiplierCount <= 0)
        {
            timeSummonFaster -= Time.deltaTime * .1f;
            timeMultiplierCount = 5f;
        }
        if (delayCounter <= 0)
        {
            enemyPoolHandler.SummonEnemy(isBeginnerMode);
            //delayCounter = summonEnemyDelay;
            delayCounter = timeSummonFaster;
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
