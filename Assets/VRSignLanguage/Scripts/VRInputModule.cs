using UnityEngine;
using UnityEngine.EventSystems;

public class VRInputModule : BaseInput
{
	[SerializeField] private Camera eventCam;

	[SerializeField]
	private OVRInput.Button clickButton = OVRInput.Button.PrimaryIndexTrigger;
	public OVRInput.Controller mainController = OVRInput.Controller.RTouch;

	protected override void Awake()
	{
		GetComponent<BaseInputModule>().inputOverride = this;
	}

	public override bool GetMouseButton(int button)
	{
		return OVRInput.Get(clickButton, mainController);
	}

	public override bool GetMouseButtonDown(int button)
	{
		return OVRInput.GetDown(clickButton, mainController);
	}

	public override bool GetMouseButtonUp(int button)
	{
		return OVRInput.GetUp(clickButton, mainController);
	}

	public override Vector2 mousePosition 
	{
		get
		{
			return new Vector2(eventCam.pixelWidth / 2, eventCam.pixelHeight / 2);
		} 
	}
}
