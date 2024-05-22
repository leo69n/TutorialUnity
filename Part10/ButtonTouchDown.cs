using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchDown : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private PlayerController PlayerControllerScript;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "Button Jump")
        {
            PlayerControllerScript.PlayerJump();
        }
        if (gameObject.name == "Button Restart")
        {
            Debug.Log("Game is restarting...");
            PlayerControllerScript.GameRestart();
        }
    }
}
