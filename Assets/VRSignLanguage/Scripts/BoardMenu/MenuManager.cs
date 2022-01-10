using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    private static MenuManager instance;
    public IMainMenu currMenu { get; private set; }
    private IMainMenu prevMenu;
    private Dictionary<MenuID, IMainMenu> cachedMenus = new Dictionary<MenuID, IMainMenu>();

    private void Awake()
    {
        instance = this;
    }

    private void OpenMenu(MenuID targetMenuId, object additionalData = null)
    {
        Debug.Log(cachedMenus.Count + " ---- " + targetMenuId.ToString());
        if (cachedMenus.Count > 0)
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
                if (!cachedMenus.ContainsKey(targetMenuId))
                {
                    cachedMenus.Add(targetMenuId, Instantiate(Resources.Load<GameObject>("Menu/" + targetMenuId.ToString())).GetComponent<IMainMenu>());
                    //cachedMenus[targetMenuId].OnRequestingOpenMenu += OpenMenu;
                }
                cachedMenus[targetMenuId].Show();
                cachedMenus[targetMenuId].Initialize(additionalData);
                currMenu.Hide();

                prevMenu = currMenu;
                currMenu = cachedMenus[targetMenuId];
            }

        }
        else
        {
            cachedMenus.Add(targetMenuId, Instantiate(Resources.Load<GameObject>("Menu/" + targetMenuId.ToString())).GetComponent<IMainMenu>());
            //cachedMenus[targetMenuId].OnRequestingOpenMenu += OpenMenu;
            cachedMenus[targetMenuId].Show();
            cachedMenus[targetMenuId].Initialize(additionalData);

            currMenu = cachedMenus[targetMenuId];
        }
    }

    public static void OpenMenu_Static(MenuID targetMenuId, object additionalData = null)
    {
        instance.OpenMenu(targetMenuId, additionalData);
    }

    // private void OnDestroy()
    // {
    // 	foreach (KeyValuePair<MenuID, IMainMenu> menu in cachedMenus)
    // 	{
    // 		menu.Value.OnRequestingOpenMenu -= OpenMenu;
    // 	}
    // }
}
