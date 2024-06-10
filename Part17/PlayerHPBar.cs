using UnityEngine;

public class PlayerHPBar : MonoBehaviour
{
    public RectTransform RectTransformComponent;
    int HPBarWidthMax = 1000; //your HP Bar Width maximum value
    int PlayerHPMax = 100; //your PlayerHP maximum value
    public void UpdatePlayerHPBar(int PlayerHP)
    {
        // new HPBar Width = current Player HP * HPBar Width Max / Player HP Max
        float newHPBarWidth = PlayerHP * HPBarWidthMax / PlayerHPMax;
        RectTransformComponent.sizeDelta = new Vector2(newHPBarWidth, RectTransformComponent.sizeDelta.y);
    }
}
