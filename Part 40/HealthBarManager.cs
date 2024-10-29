using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public SpriteRenderer SpriteRendererFill;
    float SizeHeight;
    private void Awake()
    {
        SizeHeight = SpriteRendererFill.size.y;
    }
    public void SetHealth(float CurrentHealth, float MaxHealth) 
    {
        SpriteRendererFill.size = new Vector2(CurrentHealth / MaxHealth, SizeHeight); // Width : value from 0-1
    }
}
