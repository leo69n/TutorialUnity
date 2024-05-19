using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    private string CurrentAnimation = "";

    public bool OnGround = false;
    public float MoveSpeed = 1;
    public float JumpStrength = 1;
    public Rigidbody2D PlayerRigidbody2D;
    public SpriteRenderer PlayerSpriteRenderer;
    public Animator PlayerAnimator;
    void Update()
    {
        if (Input.GetKey("d"))
        {
            PlayerMove(VectorToRight);
            RotatePlayer(true);
        }
        else if (Input.GetKey("a"))
        {
            PlayerMove(VectorToLeft);
            RotatePlayer(false);
        }
        else 
        {
            AnimationStop();
        }

        if (Input.GetKeyDown("space") == true && OnGround == true)
        {
            PlayerRigidbody2D.AddForce(new Vector2(0, 1) * JumpStrength, ForceMode2D.Impulse);
            StartCoroutine(AnimationJump());
        }
    }

    void PlayerMove(Vector2 MoveVector)
    {
        Vector2 NewMoveVector = new Vector2(MoveVector.x * MoveSpeed, PlayerRigidbody2D.velocity.y);
        PlayerRigidbody2D.velocity = NewMoveVector;

        if (OnGround == true)
        {
            PlayingAnimation("Player Walk");
        }
        
    }
    void RotatePlayer(bool Bool_Value)
    {
        PlayerSpriteRenderer.flipX = Bool_Value;
    }

    void AnimationStop()
    {
        if (OnGround == true)
        {
            PlayingAnimation("Player Idle");
        }
    }
    IEnumerator AnimationJump()
    {
        yield return new WaitForSeconds(0.1f); 
        PlayingAnimation("Player Jump");
    }
    void PlayingAnimation(string AnimationName)
    {
        if (CurrentAnimation != AnimationName)
        {
            CurrentAnimation = AnimationName;
            PlayerAnimator.Play(CurrentAnimation);
            Debug.Log("Playing: "+ CurrentAnimation);
        }
    }
}
