using UnityEngine;

public class ControllerData
{
	public Vector3 controllerPos { get; private set; }
	public bool primaryButton { get; private set; }
	public bool secondaryButton { get; private set; }
	public bool thumbstickTouch { get; private set; }
	public float triggerButton { get; private set; }
	public float gripButton { get; private set; }
	public HandGestures handGestures { get; private set; }

	public ControllerData()
	{

	}

	public ControllerData(Vector3 pos, bool primaryButtonValue, bool secondaryButtonValue, float triggerButtonValue, float gripButtonValue, bool thumbTouchValue)
	{
		SetControllerPos(pos);
		SetPrimaryButton(primaryButtonValue);
		SetSecondaryButton(secondaryButtonValue);
		SetTriggerButton(triggerButtonValue);
		SetGripButton(gripButtonValue);
		SetThumbTouchValue(thumbTouchValue);
		SetHandGestures(HandGestures.None);
	}
	public ControllerData(Vector3 pos, bool primaryButtonValue, bool secondaryButtonValue, float triggerButtonValue, float gripButtonValue, bool thumbTouchValue, HandGestures handGesturesValue)
	{
		SetControllerPos(pos);
		SetPrimaryButton(primaryButtonValue);
		SetSecondaryButton(secondaryButtonValue);
		SetTriggerButton(triggerButtonValue);
		SetGripButton(gripButtonValue);
		SetThumbTouchValue(thumbTouchValue);
		SetHandGestures(handGesturesValue);
	}

	public ControllerData(ControllerData value)
	{
		primaryButton = value.primaryButton;
		secondaryButton = value.secondaryButton;
		triggerButton = value.triggerButton;
		gripButton = value.gripButton;
		thumbstickTouch = value.thumbstickTouch;
		handGestures = value.handGestures;
	}

	public void SetControllerPos(Vector3 value)
	{
		controllerPos = value;
	}
	public void SetPrimaryButton(bool value)
	{
		primaryButton = value;
	}
	public void SetSecondaryButton(bool value)
	{
		secondaryButton = value;
	}
	public void SetTriggerButton(float value)
	{
		triggerButton = value;
	}
	public void SetGripButton(float value)
	{
		gripButton = value;
	}
	public void SetThumbTouchValue(bool value)
	{
		thumbstickTouch = value;
	}
	public void SetHandGestures(HandGestures value)
	{
		handGestures = value;
	}
}
