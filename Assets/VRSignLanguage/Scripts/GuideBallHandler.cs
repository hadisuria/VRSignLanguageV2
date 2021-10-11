using System.Collections.Generic;
using UnityEngine;

public class GuideBallHandler : MonoBehaviour
{
    private GameManager gameManager;

    private VRInputHandler inputHandler;
    private ControllerData leftController;
    private ControllerData rightController;
    private Transform playerLeftController;
    private Transform playerRightController;

    private List<GameObject> spawnedBall = new List<GameObject>();
    private List<Vector3> leftGuideBall = new List<Vector3>();
    private List<Vector3> rightGuideBall = new List<Vector3>();
    private readonly List<Vector3> calculatedLeftGuideBall = new List<Vector3>();
    private readonly List<Vector3> calculatedRightGuideBall = new List<Vector3>();

    [SerializeField] private float saveIntervalTime = 0.1f;
    private float leftCounter = 0f;
    private float rightCounter = 0f;
    [SerializeField] private bool holdToSave = true;

    private enum Controller
	{
        Left,
        Right
	}

	private void Awake()
	{
        gameManager = FindObjectOfType<GameManager>();
        inputHandler = FindObjectOfType<VRInputHandler>();
        playerLeftController = GameObject.Find("LeftControllerAnchor").transform;
        playerRightController = GameObject.Find("RightControllerAnchor").transform;
	}

	public void ResetList()
	{
        leftGuideBall.Clear();
        rightGuideBall.Clear();
        calculatedRightGuideBall.Clear();
        calculatedLeftGuideBall.Clear();
        for(int i = 0; i < spawnedBall.Count; i++)
		{
            Destroy(spawnedBall[i]);
		}
        spawnedBall.Clear();
	}

	private void LateUpdate()
	{
        leftController = inputHandler.GetLeftHandController();
        rightController = inputHandler.GetRightHandController();

		if (holdToSave)
		{
		    if (leftController.secondaryButton)
		    {
                leftCounter -= Time.deltaTime;
                if(leftCounter < 0f)
			    {
                    SpawnGuideBall(Controller.Left);
                    leftCounter = saveIntervalTime;
			    }
		    }
            if (rightController.secondaryButton)
            {
                rightCounter -= Time.deltaTime;
                if(rightCounter < 0f)
			    {
                    SpawnGuideBall(Controller.Right);
                    rightCounter = saveIntervalTime;
			    }
            }
		}
		else
		{
            if (leftController.secondaryButton)
            {
                SpawnGuideBall(Controller.Left);
            }
            if (rightController.secondaryButton)
            {
                SpawnGuideBall(Controller.Right);
            }
        }
    }

	private void SpawnGuideBall(Controller target)
    {
        if(target == Controller.Left)
		{
            spawnedBall.Add(Instantiate(Resources.Load<GameObject>("GuideBallLeft")));
            //spawnedBall[spawnedBall.Count - 1].transform.position = leftController.controllerPos;
            spawnedBall[spawnedBall.Count - 1].transform.position = playerLeftController.position;
            leftGuideBall.Add(spawnedBall[spawnedBall.Count - 1].transform.position);
        }
		else
		{
            spawnedBall.Add(Instantiate(Resources.Load<GameObject>("GuideBallRight")));
            //spawnedBall[spawnedBall.Count - 1].transform.position = rightController.controllerPos;
            spawnedBall[spawnedBall.Count - 1].transform.position = playerRightController.position;
            rightGuideBall.Add(spawnedBall[spawnedBall.Count - 1].transform.position);
        }
    }

	public (List<Vector3> left, List<Vector3> right) CalculateOffset()
	{
		for (int i = 0; i < leftGuideBall.Count; i++)
		{
			calculatedLeftGuideBall.Add(new Vector3(
                leftGuideBall[i].x / gameManager.savedCalibratedData.maxXReach, 
                leftGuideBall[i].y / gameManager.savedCalibratedData.headLevelHeight, 
                leftGuideBall[i].z / gameManager.savedCalibratedData.maxHandDistance));
		}
		for (int i = 0; i < rightGuideBall.Count; i++)
		{
			calculatedRightGuideBall.Add(new Vector3(
                rightGuideBall[i].x / gameManager.savedCalibratedData.maxXReach, 
                rightGuideBall[i].y / gameManager.savedCalibratedData.headLevelHeight, 
                rightGuideBall[i].z / gameManager.savedCalibratedData.maxHandDistance));
		}
        return (calculatedLeftGuideBall, calculatedRightGuideBall);
	}
}
