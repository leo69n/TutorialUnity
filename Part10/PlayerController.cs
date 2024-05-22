using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    private string CurrentAnimation = "";

    public bool isPressedButtonRight = false;
    public bool isPressedButtonLeft = false;

    public bool OnGround = false;
    public float MoveSpeed = 1;
    public float JumpStrength = 1;
    public Rigidbody2D PlayerRigidbody2D;
    public SpriteRenderer PlayerSpriteRenderer;
    public Animator PlayerAnimator;

    void Update()
    {
        //KeyboardMovement();
        TouchMovement();
    }
    void TouchMovement()
    {
        if (isPressedButtonRight == true)
        {
            PlayerMoveRight();
        }
        else if (isPressedButtonLeft == true)
        {
            PlayerMoveLeft();
        }
        else
        {
            PlayerStopMovement();
        }
    }
    void KeyboardMovement()
    {
        if (Input.GetKey("d"))
        {
            PlayerMoveRight();
        }
        else if (Input.GetKey("a"))
        {
            PlayerMoveLeft();
        }
        else
        {
            PlayerStopMovement();
        }

        if (Input.GetKeyDown("space") == true)
        {
            PlayerJump();
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
        }
    }

    public void PlayerMoveRight()
    {
        PlayerMove(VectorToRight);
        RotatePlayer(true);
    }
    public void PlayerMoveLeft()
    {
        PlayerMove(VectorToLeft);
        RotatePlayer(false);
    }
    public void PlayerStopMovement()
    {
        AnimationStop();
    }
    public void PlayerJump()
    {
        if (OnGround == true)
        {
            PlayerRigidbody2D.AddForce(new Vector2(0, 1) * JumpStrength, ForceMode2D.Impulse);
            StartCoroutine(AnimationJump());
        }
    }
    public void GameRestart()
    {
        string CurrentSceneName = gameObject.scene.name;
        SceneManager.LoadScene(CurrentSceneName);
    }
}