using UnityEngine;

public class CenterPointTeleporter : MonoBehaviour, IInteractableObject
{
	[SerializeField] private Transform player;

	public void ExecuteInteractHit()
	{
		player.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
	}
}
