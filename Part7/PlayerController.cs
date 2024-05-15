using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);

    public float MoveSpeed = 1;
    public float JumpStrength = 1;
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
        if (Input.GetKeyDown("space"))
        {
            PlayerRigidbody2D.AddForce(new Vector2(0,1) * JumpStrength, ForceMode2D.Impulse);
        }
    }

    void PlayerMove(Vector2 MoveVector)
    {
        Vector2 NewMoveVector = new Vector2(MoveVector.x * MoveSpeed, PlayerRigidbody2D.velocity.y);
        PlayerRigidbody2D.velocity = NewMoveVector;
    }
    void RotatePlayer(bool Bool_Value)
    {
        PlayerSpriteRenderer.flipX = Bool_Value;
    }
}
