using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Vector2 VectorToRight = new Vector2(1, 0);
    private Vector2 VectorToLeft = new Vector2(-1, 0);
    private string CurrentAnimation = "";

    public float MoveSpeed = 1;
    public float WalkSpeed = 1;
    public float RunSpeed = 2;

    public bool OnGround = false;
    public float JumpStrength = 1;
    public Rigidbody2D PlayerRigidbody2D;
    public SpriteRenderer PlayerSpriteRenderer;
    public Animator PlayerAnimator;

    public bool isPressedButtonRight = false;
    public bool isPressedButtonLeft = false;

    public int HealthPoint = 1;
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(0,10);
    }
    void Update()
    {
        KeyboardController();
        //TouchController();
    }
    void KeyboardController()
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

        
        if (Input.GetKey("left shift") == true && OnGround == true)
        {
            PlayerRunOn();
        }
        if (Input.GetKeyUp("left shift") == true)
        {
            PlayerRunOff();
        }
        
    }
    public void PlayerRunOn()
    {
        if (MoveSpeed != RunSpeed)
        {
            StartCoroutine(ChangePlayerSpeed(RunSpeed));
        }
    }
    public void PlayerRunOff()
    {
        if (MoveSpeed != WalkSpeed)
        {
            StartCoroutine(ChangePlayerSpeed(WalkSpeed));
        }
    }
    IEnumerator ChangePlayerSpeed(float NewSpeed)
    {
        yield return new WaitUntil(()=> OnGround == true);
        MoveSpeed = NewSpeed;
    }
    void TouchController()
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
    void PlayerMove(Vector2 MoveVector)
    {
        Vector2 NewMoveVector = new Vector2(MoveVector.x * MoveSpeed, PlayerRigidbody2D.velocity.y);
        PlayerRigidbody2D.velocity = NewMoveVector;

        if (OnGround == true)
        {
            if (MoveSpeed == WalkSpeed)
            {
                PlayingAnimation("Player Walk");
            }
            if (MoveSpeed == RunSpeed)
            {
                PlayingAnimation("Player Run");
            }
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

    public void PlayerHealthPointUpdate(int AddingValue)
    {
        HealthPoint = HealthPoint + AddingValue;
        PlayerPrefs.SetInt("PlayerHP", HealthPoint);
        PlayerPrefs.Save();//call this when game is loading or player no input...
    }

    private void Start()
    {
        HealthPoint = PlayerPrefs.GetInt("PlayerHP",100);
    }
}
