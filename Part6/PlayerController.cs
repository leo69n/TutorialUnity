using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);

    public float MoveSpeed = 1;
    public Rigidbody2D PlayerRigidbody2D;
    public SpriteRenderer PlayerSpriteRenderer;
    void Update()
    {
        if (Input.GetKey("d"))
        {
            PlayerMove(VectorToRight);
            RotatePlayer(true);
        }
        if (Input.GetKey("a"))
        {
            PlayerMove(VectorToLeft);
            RotatePlayer(false);
        }
    }

    void PlayerMove(Vector2 MoveVector)
    {
        PlayerRigidbody2D.velocity = MoveVector * MoveSpeed;
    }
    void RotatePlayer(bool Bool_Value)
    {
        PlayerSpriteRenderer.flipX = Bool_Value;
    }
}
