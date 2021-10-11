using System.Collections.Generic;
using UnityEngine;

public class HandAnimManager : MonoBehaviour
{
	private enum Hand 
	{
		LeftHand,
		Righthand
	}

	[SerializeField] private VRInputHandler inputHandler;
	[SerializeField] private Hand handTarget;
	[SerializeField] private Animator handAnimator;
	private Dictionary<string, float> animatorParametersList = new Dictionary<string, float>();

	private const float changeValue = .125f;
	private const float lowThreshold = .2f;
	private const float highThreshold = .9f;

	// Start is called before the first frame update
	//void Start()
	//{
	//	switch (handTarget)
	//	{
	//		case Hand.LeftHand:
	//			inputHandler.LeftControllerInput += UpdateHandAnimation;
	//			break;
	//		case Hand.Righthand:
	//			inputHandler.RightControllerInput += UpdateHandAnimation;
	//			break;
	//	}
	//}

	private void LateUpdate()
	{
		//updating animation based on input from input handler
		switch (handTarget)
		{
			case Hand.LeftHand:
				UpdateHandAnimation(inputHandler.GetLeftHandController());
				break;
			case Hand.Righthand:
				UpdateHandAnimation(inputHandler.GetRightHandController());
				break;
		}
		//anim transition
		if (animatorParametersList.Count > 0)
		{
			foreach(var param in animatorParametersList)
			{
				if (handAnimator.GetFloat(param.Key) != param.Value)
					CalculateAnimationFloat(param.Key, param.Value);
			}
		}
	}

	private void UpdateHandAnimation(ControllerData controller)
	{
		if (controller.thumbstickTouch)
			UpdateAnimParamDictionary("Button", 1f);
			//handAnimator.SetFloat("Button", 1f);
		else
			UpdateAnimParamDictionary("Button", 0f);
			//handAnimator.SetFloat("Button", 0f);

		if (controller.triggerButton >= lowThreshold)
			UpdateAnimParamDictionary("Trigger", 1f);
			//handAnimator.SetFloat("Trigger", 1f);
		else
			UpdateAnimParamDictionary("Trigger", 0f);
			//handAnimator.SetFloat("Trigger", 0f);

		if (controller.gripButton >= highThreshold)
			UpdateAnimParamDictionary("Grip", 1f);
			//handAnimator.SetFloat("Grip", 1f);
		else if (controller.gripButton >= lowThreshold && controller.gripButton < highThreshold)
			UpdateAnimParamDictionary("Grip", .5f);
		else
			UpdateAnimParamDictionary("Grip", 0f);
			//handAnimator.SetFloat("Grip", 0f);
	}

	private void UpdateAnimParamDictionary(string key, float value)
	{
		if (!animatorParametersList.ContainsKey(key))
		{
			animatorParametersList.Add(key, value);
		}
		else
		{
			animatorParametersList[key] = value;
		}
	}

	private void CalculateAnimationFloat(string animParamName, float target)
	{
		if (handAnimator.GetFloat(animParamName) > target)
		{
			handAnimator.SetFloat(animParamName, handAnimator.GetFloat(animParamName) - changeValue);
		}
		else if (handAnimator.GetFloat(animParamName) < target)
		{
			handAnimator.SetFloat(animParamName, handAnimator.GetFloat(animParamName) + changeValue);
		}
	}

	//private void OnDestroy()
	//{
	//	switch (handTarget)
	//	{
	//		case Hand.LeftHand:
	//			inputHandler.LeftControllerInput -= UpdateHandAnimation;
	//			break;
	//		case Hand.Righthand:
	//			inputHandler.RightControllerInput -= UpdateHandAnimation;
	//			break;
	//	}
	//}
}
