using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public IMainMenu currMenu { get; private set; }
	private IMainMenu prevMenu;
	private Dictionary<MenuID, IMainMenu> cachedBoardMenus = new Dictionary<MenuID, IMainMenu>();

	public void OpenMenu(MenuID targetMenuId, object additionalData = null)
	{
		if (cachedBoardMenus.Count > 0)
		{
			if (currMenu.menuID == targetMenuId)
			{
				currMenu.Hide();
				currMenu.Show();
				currMenu.Initialize(additionalData);
			}
			else if (targetMenuId == MenuID.Previous)
			{
				prevMenu.Show();
				prevMenu.Initialize(additionalData);
				currMenu.Hide();

				var temp = prevMenu;
				prevMenu = currMenu;
				currMenu = temp;
			}
			else if (targetMenuId == MenuID.None)
			{
				currMenu.Hide();

				prevMenu = currMenu;
			}
			else
			{
				if (!cachedBoardMenus.ContainsKey(targetMenuId))
				{
					cachedBoardMenus.Add(targetMenuId, Instantiate(Resources.Load<GameObject>("Menu/" + targetMenuId.ToString()), transform).GetComponent<IMainMenu>());
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
			cachedBoardMenus.Add(targetMenuId, Instantiate(Resources.Load<GameObject>("Menu/" + targetMenuId.ToString()), transform).GetComponent<IMainMenu>());
			cachedBoardMenus[targetMenuId].OnRequestingOpenMenu += OpenMenu;
			cachedBoardMenus[targetMenuId].Show();
			cachedBoardMenus[targetMenuId].Initialize(additionalData);

			currMenu = cachedBoardMenus[targetMenuId];
		}
	}

	private void OnDestroy()
	{
		foreach (KeyValuePair<MenuID, IMainMenu> menu in cachedBoardMenus)
		{
			menu.Value.OnRequestingOpenMenu -= OpenMenu;
		}
	}
}
