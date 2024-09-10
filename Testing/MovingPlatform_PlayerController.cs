using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float MovingPlatformSpeed; //..

    public bool isNonLoopAnimation = false;
    public PlayerHPBar PlayerHPBarScript;

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

    public Color ColorShield;
    public Color ColorNormal;

    public int HealthPoint = 1;
    private bool CanTakeDamage = true;

    GamepadInput GamepadInputComponent;
    private void Awake()
    {
        Physics2D.IgnoreLayerCollision(0, 10);
        GamepadInputComponent = FindObjectOfType<GamepadInput>();
    }
    
    private void Start()
    {
        HealthPoint = PlayerPrefs.GetInt("PlayerHP", 100);//update HP
        PlayerHPBarScript.UpdatePlayerHPBar(HealthPoint); //Update PlayerHP to HPBar
    }

    void Update()
    {
        KeyboardController();
        //TouchController();
        //GamepadController();
    }

    void KeyboardController()
    {
        if (Input.GetKeyDown("t") == true)
        {
            PlayerAttack();
        }

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


        if (Input.GetKey("left shift") == true)
        {
            PlayerRunOn();
        }
        if (Input.GetKeyUp("left shift") == true)
        {
            PlayerRunOff();
        }
        /*
        if (Input.GetKeyDown("t") == true)
        {
            PlayerAttack();
        }
        if (Input.GetKeyDown("y") == true)
        {
            PlayerGetHit();
        }*/
    }

    void GamepadController()
    {
        if (GamepadInputComponent != null)
        {
            if (GamepadInputComponent.LeftAnalogVector2.x > 0)
            {
                PlayerMoveRight();
            }
            else if (GamepadInputComponent.LeftAnalogVector2.x < 0)
            {
                PlayerMoveLeft();
            }
            else
            {
                PlayerStopMovement();
            }

            if (GamepadInputComponent.onButtonDown["Jump"] == true)
            {
                PlayerJump();
            }


            if (GamepadInputComponent.onButtonHold["Run"] == true)
            {
                PlayerRunOn();
            }
            if (GamepadInputComponent.onButtonUp["Run"] == true)
            {
                PlayerRunOff();
            }
        }


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
    public void PlayerRunOn()
    {
        if (MoveSpeed != RunSpeed && OnGround == true)
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
        yield return new WaitUntil(() => OnGround == true);
        MoveSpeed = NewSpeed;
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
        if (CanTakeDamage == true)
        {
            CanTakeDamage = false;
            HealthPoint = HealthPoint + AddingValue;//update HP
            PlayerHPBarScript.UpdatePlayerHPBar(HealthPoint); //Update PlayerHP to HPBar
            PlayerPrefs.SetInt("PlayerHP", HealthPoint);
            PlayerPrefs.Save();

            StartCoroutine(GivePlayerShield());
        }

    }
    IEnumerator GivePlayerShield()
    {
        PlayerSpriteRenderer.color = ColorShield;
        yield return new WaitForSeconds(3);//can not take damage in 3 seconds
        CanTakeDamage = true;
        PlayerSpriteRenderer.color = ColorNormal;
    }

    void PlayerMove(Vector2 MoveVector)
    {
        //float fixSpeed = Mathf.Abs(MoveSpeed) + Mathf.Abs(MovingPlatformSpeed);
        float fixSpeed = MoveVector.x * MoveSpeed; fixSpeed = Player tVelocity
        if (MovingPlatformSpeed != 0)
        {
            fixSpeed = (MoveSpeed * MoveVector.x) + MovingPlatformSpeed; // Player Velocity + Platform Velocity
        }
        /*
        if (MoveVector.x < 0 && MovingPlatformSpeed > 0)
        {
            fixSpeed = fixSpeed * -1;
        }
        if (MoveVector.x < 0 && MovingPlatformSpeed < 0)
        {
            fixSpeed = (Mathf.Abs(MoveVector.x) + Mathf.Abs(MovingPlatformSpeed)) * -1;
            Debug.Log(fixSpeed);
        }*/
        Vector2 NewMoveVector = new Vector2( (fixSpeed), PlayerRigidbody2D.velocity.y);//..
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
    void PlayingAnimation(string AnimationName) //play loop animation
    {
        if (CurrentAnimation != AnimationName && isNonLoopAnimation == false)
        {
            CurrentAnimation = AnimationName;
            PlayerAnimator.Play(CurrentAnimation);
        }
    }
    void PlayingNonLoopAnimation(string AnimationName) //play non loop animation
    {
        if (CurrentAnimation != AnimationName)
        {
            CurrentAnimation = AnimationName;
            PlayerAnimator.Play(CurrentAnimation);
        }
    }
    IEnumerator PrepareNonLoopAnimation(string AnimationName)
    {
        isNonLoopAnimation = true;
        PlayingNonLoopAnimation(AnimationName); //this animation will be played at the end of Frame
        yield return new WaitForEndOfFrame();
        var CurrentAnimationInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (CurrentAnimationInfo.IsName(AnimationName) == true)
        {
            var AnimationDuration = CurrentAnimationInfo.length;
            yield return new WaitForSeconds(AnimationDuration);
            isNonLoopAnimation = false;
        }
        else
        {
            yield return null;
            isNonLoopAnimation = false;
        }
    }
    void PlayerAttack()
    {
        StartCoroutine(PrepareNonLoopAnimation("Attack"));
        //throw Bullet or cast Spell here
    }
}
