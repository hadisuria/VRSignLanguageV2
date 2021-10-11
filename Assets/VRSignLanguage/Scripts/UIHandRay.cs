using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LineRenderer))]
public class UIHandRay : MonoBehaviour
{
	//private enum Hand  { Left, Right }
	//[SerializeField] private Hand hand;
	[SerializeField] private float maxLength = 3f;

    [SerializeField] private EventSystem eventSystem;
    [SerializeField] private StandaloneInputModule inputModule;
	[SerializeField] private VRInputModule vrModule;

    private LineRenderer lineRenderer;

	private void Awake()
	{
        lineRenderer = GetComponent<LineRenderer>();
	}

	private void LateUpdate()
	{
		//switch (hand)
		//{
		//	case Hand.Left:
		//		if (GameManager.isRayActive && vrModule.mainController == OVRInput.Controller.LTouch)
		//		{
		//			lineRenderer.enabled = true;
		//			UpdateLineLength();
		//		}
		//		else
		//		{
		//			lineRenderer.enabled = false;
		//		}
		//		break;
		//	case Hand.Right:
		//		if (GameManager.isRayActive && vrModule.mainController == OVRInput.Controller.RTouch)
		//		{
		//			lineRenderer.enabled = true;
		//			UpdateLineLength();
		//		}
		//		else
		//		{
		//			lineRenderer.enabled = false;
		//		}
		//		break;
		//}

		if (GameManager.isRayActive)
		{
			lineRenderer.enabled = true;
			UpdateLineLength();
		}
		else
		{
			lineRenderer.enabled = false;
		}
	}

	private void UpdateLineLength()
	{
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, GetEndPos());
	}

	private Vector3 GetEndPos()
	{
		float distance = GetCanvasDistance();
		Vector3 endPos = CalculateEndPos(maxLength);

		if (distance != 0f)
			endPos = CalculateEndPos(distance);

		return endPos;
	}

	private float GetCanvasDistance()
	{
		// Get Data
		PointerEventData eventData = new PointerEventData(eventSystem);
		eventData.position = inputModule.inputOverride.mousePosition;

		// Raycast
		List<RaycastResult> results = new List<RaycastResult>();
		eventSystem.RaycastAll(eventData, results);

		// Get Closest
		RaycastResult closestResult = GetFirstResult(results);
		float distance = closestResult.distance;

		return Mathf.Clamp(distance, 0f, maxLength);
	}

	private RaycastResult GetFirstResult(List<RaycastResult> results)
	{
		if(results.Count > 0)
		{
			foreach(RaycastResult r in results)
			{
				if (!r.gameObject)
				{
					continue;
				}

				return r;
			}
		}

		return new RaycastResult();
	}

	private Vector3 CalculateEndPos(float length)
	{
		return transform.position + (transform.forward * length);
	}
}
