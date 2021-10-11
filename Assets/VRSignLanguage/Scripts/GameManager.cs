using System.Diagnostics;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
	[SerializeField] private BodyCalibrator calibrator;
	[SerializeField] private VRInputHandler inputHandler;
	[SerializeField] private ButtonEvent calibrateMenuButton;
	[SerializeField] private BoardMenuController menuController;
	[SerializeField] private Transform[] keyPositions;

	private bool prevInputHandlerPrimaryButtonLeft = false;
	private bool prevInputHandlerPrimaryButtonRight = false;
	public static bool isRayActive { get; private set; }
	[SerializeField] private VRInputModule vrInputModule;
	public event Action OnFinishCalibrate;

	//calibrated data
	public float maxHandDistance { get; private set; }
	public float headLevelHeight { get; private set; }
	public float bellyHeight { get; private set; }
	public Vector3 leftShoulderOffset { get; private set; }
	public Vector3 rightShoulderOffset { get; private set; }
	public float maxXReach { get; private set; }

	// To store saved / loaded calibratedData
	public SavedCalibratedData savedCalibratedData { get; private set; }


	// testing
	public SignLanguageDictionary languageDictionary = new SignLanguageDictionary();
 

	private void Awake()
	{
		SaveSystem.Init();
		calibrateMenuButton.OnButtonClicked += ShowCalibrationMenu;
		Load();

		// Load Sign Language Dictionary Data
		languageDictionary.LoadData();
	} 

	private void Update()
	{
		
		if (menuController.currMenu.menuID == BoardMenuID.CalibrateMenu)
		{
			if (inputHandler.GetRightHandController().secondaryButton && inputHandler.GetLeftHandController().secondaryButton)
				StartCalibrate();
		}

		HandleRayActive();
		

		// OUTDATED DELETE LATER
		// // if (Input.GetMouseButtonDown(0))
		// // {
		// // 	RaycastHit hit;
		// // 	Ray rayLineOut = Camera.main.ScreenPointToRay(Input.mousePosition);
		// // 	if (Physics.Raycast(rayLineOut, out hit))
		// // 	{
		// // 		// set the game object to the gameObject that the raycast hit
		// // 		var pointedObject = hit.collider.gameObject;
		// // 		if (pointedObject.TryGetComponent<IInteractableObject>(out var target))
		// // 		{
		// // 			target.ExecuteInteractHit();
		// // 		}
		// // 	}
		// // }
	}

	private void HandleRayActive() {
		bool rightPrimaryButton =  inputHandler.GetRightHandController().primaryButton;
		bool leftPrimaryButton = inputHandler.GetLeftHandController().primaryButton;


		if(vrInputModule.mainController == OVRInput.Controller.RTouch)
		{
			if (rightPrimaryButton)
			{
				if (!prevInputHandlerPrimaryButtonRight)
				{
					prevInputHandlerPrimaryButtonRight = true; ;
					// outdated
					// // if (!isRayActive)
					// // 	isRayActive = true;
					// // else
					// // 	isRayActive = false;
					// Simplified Code
					isRayActive = !isRayActive;
				}
			}
			else
			{
				prevInputHandlerPrimaryButtonRight = false;
			}
		}
		else if(vrInputModule.mainController == OVRInput.Controller.LTouch)
		{
			if (leftPrimaryButton)
			{
				if (!prevInputHandlerPrimaryButtonLeft)
				{
					prevInputHandlerPrimaryButtonLeft = true;
					// outdated
					// // if (!isRayActive)
					// // 	isRayActive = true;
					// // else
					// // 	isRayActive = false;
					// Simplified Code
					isRayActive = !isRayActive;
				}
			}
			else
			{
				prevInputHandlerPrimaryButtonLeft = false;
			}
		}

		//if (inputHandler.GetRightHandController().primaryButton)
		//{
		//	if (!isRayActive)
		//		isRayActive = true;
		//	else if (isRayActive && vrInputModule.mainController != OVRInput.Controller.RTouch)
		//		isRayActive = true;
		//	else
		//		isRayActive = false;

		//	vrInputModule.mainController = OVRInput.Controller.RTouch;
		//}
		//else if (inputHandler.GetLeftHandController().primaryButton)
		//{
		//	if (!isRayActive)
		//		isRayActive = true;
		//	else if (isRayActive && vrInputModule.mainController != OVRInput.Controller.LTouch)
		//		isRayActive = true;
		//	else
		//		isRayActive = false;

		//	vrInputModule.mainController = OVRInput.Controller.LTouch;
		//}
	}

	private void SaveCalibratedData(SavedCalibratedData savedCalibratedDataObj)
	{
		string json = JsonUtility.ToJson(savedCalibratedDataObj);
		SaveSystem.SaveData(json, SaveSystem.SAVE_CALIBRATION);
	}

	private void Load()
	{
		// load the callibrated data if any
		string saveString = SaveSystem.LoadData(SaveSystem.SAVE_CALIBRATION);
		if(saveString != null)
		{
			SavedCalibratedData savedCalibratedDataObj = JsonUtility.FromJson<SavedCalibratedData>(saveString);
			// Assign data to saved calibrated data
			savedCalibratedData = new SavedCalibratedData(savedCalibratedDataObj);

			menuController.OpenMenu(BoardMenuID.MainMenu);
		}
		else
		{
			ShowCalibrationMenu();
		}
	}

	private void ShowCalibrationMenu()
	{
		menuController.OpenMenu(BoardMenuID.CalibrateMenu);
	}

	private void StartCalibrate()
	{
        var calibratedValue = calibrator.CalibratePosition();
        maxHandDistance = calibratedValue.handLength;
        headLevelHeight = calibratedValue.bodyHeight;
		bellyHeight = calibratedValue.bellyHeight;
		leftShoulderOffset = calibratedValue.shoulderOffsetLeft;
		rightShoulderOffset = calibratedValue.shoulderOffsetRight;
		maxXReach = calibratedValue.maxXReach;

		// Store calibrated data to local variable
		savedCalibratedData = new SavedCalibratedData(
				maxHandDistance, 
				headLevelHeight, 
				bellyHeight, 
				leftShoulderOffset, 
				rightShoulderOffset,
				maxXReach
			);

		SaveCalibratedData(savedCalibratedData);
		OnFinishCalibrate?.Invoke();

#if UNITY_EDITOR
		// visualize calibrated pos using simple game object
		// index 0 = player hmd pos
		// index 1 = head indicator
		keyPositions[1].position = new Vector3(keyPositions[0].position.x, headLevelHeight, keyPositions[0].position.z);

		// inedx 2 = belly indicator
		keyPositions[2].position = new Vector3(keyPositions[0].position.x, bellyHeight, keyPositions[0].position.z);

		// index 3 = left shoulder indicator
		keyPositions[3].position = new Vector3(keyPositions[0].position.x + leftShoulderOffset.x, leftShoulderOffset.y, keyPositions[0].position.z);

		// index 4 = right shoulder indicator
		keyPositions[4].position = new Vector3(keyPositions[0].position.x + rightShoulderOffset.x, rightShoulderOffset.y, keyPositions[0].position.z);
#else
		keyPositions[0].gameObject.SetActive(false);
		keyPositions[1].gameObject.SetActive(false);
		keyPositions[2].gameObject.SetActive(false);
		keyPositions[3].gameObject.SetActive(false);
		keyPositions[4].gameObject.SetActive(false);
#endif
	}

	private void OnDestroy()
	{
		calibrateMenuButton.OnButtonClicked -= ShowCalibrationMenu;
	}
}
