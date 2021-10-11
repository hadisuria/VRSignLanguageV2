using System.Collections.Generic;
using UnityEngine;

public class BoardMenuController : MonoBehaviour
{
    public IBoardMenu currMenu { get; private set; }
    private IBoardMenu prevMenu;
    private Dictionary<BoardMenuID, IBoardMenu> cachedBoardMenus = new Dictionary<BoardMenuID, IBoardMenu>();

	public void OpenMenu(BoardMenuID targetMenuId, object additionalData = null)
	{
		if(cachedBoardMenus.Count > 0)
		{
			if(currMenu.menuID == targetMenuId)
			{
				currMenu.Hide();
				currMenu.Show();
				currMenu.Initialize(additionalData);
			}
			else if(targetMenuId == BoardMenuID.Previous)
			{
				prevMenu.Show();
				prevMenu.Initialize(additionalData);
				currMenu.Hide();

				var temp = prevMenu;
				prevMenu = currMenu;
				currMenu = temp;
			}
			else
			{
				if (!cachedBoardMenus.ContainsKey(targetMenuId))
				{
					cachedBoardMenus.Add(targetMenuId, Instantiate(Resources.Load<GameObject>("Menu/" + targetMenuId.ToString()), transform).GetComponent<IBoardMenu>());
					cachedBoardMenus[targetMenuId].OnRequestingOpenMenu += OpenMenu;
				}
				cachedBoardMenus[targetMenuId].Show();
				cachedBoardMenus[targetMenuId].Initialize(additionalData);
				currMenu.Hide();

				prevMenu = currMenu;
				currMenu = cachedBoardMenus[targetMenuId];
			}

		}
		else
		{
			cachedBoardMenus.Add(targetMenuId, Instantiate(Resources.Load<GameObject>("Menu/" + targetMenuId.ToString()), transform).GetComponent<IBoardMenu>());
			cachedBoardMenus[targetMenuId].OnRequestingOpenMenu += OpenMenu;
			cachedBoardMenus[targetMenuId].Show();
			cachedBoardMenus[targetMenuId].Initialize(additionalData);

			currMenu = cachedBoardMenus[targetMenuId];
		}
	}

	private void OnDestroy()
	{
		foreach(KeyValuePair<BoardMenuID, IBoardMenu> menu in cachedBoardMenus)
		{
			menu.Value.OnRequestingOpenMenu -= OpenMenu;
		}
	}
}
