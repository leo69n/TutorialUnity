using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);

    public Rigidbody2D PlayerRigidbody2D;
    void Update()
    {
        if (Input.GetKey("d"))
        {
            PlayerRigidbody2D.velocity = VectorToRight;
        }

        if (Input.GetKey("a"))
        {
            PlayerRigidbody2D.velocity = VectorToLeft;
        }
    }
}
