using UnityEngine;

public class DamageText : MonoBehaviour
{
    Rigidbody2D Rigidbody2DComponent;
    float MoveSpeed = 0.5f;
    private void Awake()
    {
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        Rigidbody2DComponent.velocity = new Vector2(0,1) * MoveSpeed;
        Destroy(this.gameObject, 2);
    }
}
