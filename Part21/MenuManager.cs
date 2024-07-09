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
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Button Up is pressed!");
            SelectPreviousMenu();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Button Down is pressed!");
            SelectNextMenu();
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Button Enter is pressed!");
            if (MenuName == "Menu-StartGame")
            {
                SceneManager.LoadScene("Level1");
            }
            if (MenuName == "Menu-Settings")
            {
                SceneManager.LoadScene("Settings");
            }
            if (MenuName == "Menu-Exit")
            {
                Application.Quit();
            }
        }
    }
    
}
