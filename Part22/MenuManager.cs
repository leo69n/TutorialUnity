using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject SelectIcon;
    public List<GameObject> MenuList;
    int MenuIndex = 0;
    public string MenuName = "Menu-StartGame";
    void SelectNextMenu()
    {
        MenuIndex = MenuIndex + 1;
        if (MenuIndex >= MenuList.Count)
        {
            MenuIndex = 0;
        }
        GameObject SelectingMenu = MenuList[MenuIndex];
        SelectIcon.transform.position = new Vector2(SelectIcon.transform.position.x, SelectingMenu.transform.position.y);
        MenuName = SelectingMenu.name;
    }
    void SelectPreviousMenu()
    {
        MenuIndex = MenuIndex - 1;
        if (MenuIndex < 0)
        {
            MenuIndex = MenuList.Count - 1;
        }
        GameObject SelectingMenu = MenuList[MenuIndex];
        SelectIcon.transform.position = new Vector2(SelectIcon.transform.position.x, SelectingMenu.transform.position.y);
        MenuName = SelectingMenu.name;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) //press Button Down Arrow
        {
            SelectPreviousMenu();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) //press Button Up Arrow
        {
            SelectNextMenu();
        }
        if (Input.GetKeyDown(KeyCode.Return)) //press Button Enter
        {
            if (MenuName == "Menu-StartGame")
            {
                ApplicationVariables.LoadingSceneName = "Level1";
                SceneManager.LoadScene("LoadingScene");
            }
            if (MenuName == "Menu-Settings")
            {
                ApplicationVariables.LoadingSceneName = "Settings";
                SceneManager.LoadScene("LoadingScene");
            }
            if (MenuName == "Menu-Exit")
            {
                Application.Quit();
            }
        }
    }

}
