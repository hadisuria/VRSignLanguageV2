using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler//, IPointerClickHandler
{
	public event Action OnButtonClicked;

	[SerializeField] private Graphic targetGraphic;

	[SerializeField] private Color normalColor = Color.white;
	[SerializeField] private Color hoverColor = new Color(.85f, .85f, .85f, 1f);
	[SerializeField] private Color downColor = new Color(.7f, .7f, .7f, 1f);

	private bool click = false;

	//public void OnPointerClick(PointerEventData eventData)
	//{
	//	if(GameManager.isRayActive)
	//		OnButtonClicked?.Invoke();
	//}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (GameManager.isRayActive)
		{
			targetGraphic.color = downColor;
			click = true;
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (GameManager.isRayActive)
			targetGraphic.color = hoverColor;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (GameManager.isRayActive)
		{
			targetGraphic.color = normalColor;
			if (click)
			{
				click = false;
			}
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (GameManager.isRayActive)
		{
			targetGraphic.color = hoverColor;
			if (click)
			{
				OnButtonClicked?.Invoke();
				click = false;
			}
		}
	}
}
