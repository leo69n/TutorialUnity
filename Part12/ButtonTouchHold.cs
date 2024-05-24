using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonTouchHold : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isHold = false;
    private PlayerController PlayerControllerScript;

    private void Awake()
    {
        PlayerControllerScript = FindObjectOfType<PlayerController>();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHold = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(TouchEnd());
    }

    private void Update()
    {
        if (isHold == true)
        {
            if (name == "Button Right")
            {
                PlayerControllerScript.isPressedButtonRight = true;
            }
            if (name == "Button Left")
            {
                PlayerControllerScript.isPressedButtonLeft = true;
            }
            if (name == "Button Run")
            {
                PlayerControllerScript.PlayerRunOn();
            }
        }
    }

    IEnumerator TouchEnd()
    {
        yield return new WaitForSeconds(0.05f);
        isHold = false;
        if (name == "Button Right")
        {
            PlayerControllerScript.isPressedButtonRight = false;
        }
        if (name == "Button Left")
        {
            PlayerControllerScript.isPressedButtonLeft = false;
        }
        if (name == "Button Run")
        {
            PlayerControllerScript.PlayerRunOff();
        }
    }
}