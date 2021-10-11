using System;
using UnityEngine;

public class MenuButton : MonoBehaviour,IInteractableObject
{
	public event Action OnButtonHit;

	public void ExecuteInteractHit()
	{
		OnButtonHit?.Invoke();
	}
}
