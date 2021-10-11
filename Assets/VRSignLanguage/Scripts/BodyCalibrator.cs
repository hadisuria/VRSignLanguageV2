using UnityEngine;

public class BodyCalibrator : MonoBehaviour
{
	[SerializeField] private VRInputHandler inputHandler;
	[SerializeField] private Transform playerHMD;
	[SerializeField] private Transform playerLeftController;
	[SerializeField] private Transform playerRightController;

	public (float bodyHeight, float handLength, Vector3 shoulderOffsetLeft, Vector3 shoulderOffsetRight, float bellyHeight, float maxXReach) CalibratePosition()
	{
		//var leftHand = inputHandler.GetLeftHandController().controllerPos;
		//var rightHand = inputHandler.GetRightHandController().controllerPos;
		var leftHand = playerLeftController.position;
		var rightHand = playerRightController.position;

		float bodyHeight = GetBodyHeight(playerHMD.position);
		float handLength = GetHandLength(playerHMD.position, leftHand, rightHand);
		(Vector3 shoulderOffsetLeft, Vector3 shoulderOffsetRight) = GetShoulderPosition(playerHMD.position, leftHand, rightHand);
		float bellyHeight = GetBellyHeight(shoulderOffsetLeft, handLength);
		float maxXReach = handLength + shoulderOffsetRight.x;

		return (
			bodyHeight, 
			handLength, 
			shoulderOffsetLeft, 
			shoulderOffsetRight, 
			bellyHeight,
			maxXReach
		);
	}

	// calculate hand length
	private float GetHandLength(Vector3 headPos, Vector3 leftHand, Vector3 rightHand)
	{

		float leftHandLength = leftHand.z - headPos.z;
		float rightHandLength = rightHand.z - headPos.z;

		return Mathf.Abs(leftHandLength + rightHandLength) / 2f;
	}
	
	// height
	private float GetBodyHeight(Vector3 headPos)
	{
		return headPos.y;
	}

	// Get Shoulder Position
	private (Vector3 left, Vector3 right) GetShoulderPosition(Vector3 headPos, Vector3 leftHand, Vector3 rightHand)
	{

		float bodyWidth = Mathf.Abs(leftHand.x - rightHand.x);
		float shoulderHeight = Mathf.Abs(leftHand.y + rightHand.y) / 2;

		// calculate shoulder position
		Vector3 shoulderPositionLeft = new Vector3(leftHand.x, shoulderHeight, headPos.z);
		Vector3 shoulderPositionRight = new Vector3(rightHand.x, shoulderHeight, headPos.z);

		return (left: shoulderPositionLeft, right: shoulderPositionRight);
	}


	// Belly height
	private float GetBellyHeight(Vector3 shoulderOffset, float handLength) {
		// Estimate belly height based on shoulder position and handlength divided by 2

		return (shoulderOffset.y - (handLength / 2));
	}

}
