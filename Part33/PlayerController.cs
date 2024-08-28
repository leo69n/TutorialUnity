using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem SmokeRunning;
    bool isMoving = false;

    public GameObject Bullet;
    public GameObject BouncingBullet;

    public Transform Right;
    public Transform Left;

    private bool isNonLoopAnimation = false;
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
        //Physics2D.IgnoreLayerCollision(10, 0);//Ignore Player & Background when Player is in Default Layer for Ground check 
        Physics2D.IgnoreLayerCollision(10, 11);// Ignore Player & Background when Player is in Player Layer for Ground check
        Physics2D.IgnoreLayerCollision(11, 12);// Ignore Bullet & Player
        Physics2D.IgnoreLayerCollision(10, 12);// Ignore Bullet & Background
        Physics2D.IgnoreLayerCollision(12, 12);// Ignore Bullet & Bullet
        GamepadInputComponent = FindObjectOfType<GamepadInput>();
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
            isMoving = true;
        }
        else if (Input.GetKey("a"))
        {
            PlayerMoveLeft();
            isMoving = true;
        }
        else
        {
            PlayerStopMovement();
            isMoving = false;
        }

        if (Input.GetKeyDown("space") == true)
        {
            PlayerJump();
            SmokeRunning.Stop();
        }


        if (Input.GetKey("left shift") == true)
        {
            PlayerRunOn();
            if (isMoving == true && MoveSpeed == RunSpeed)
            {
                if (SmokeRunning.isPlaying == false)
                {
                    SmokeRunning.Play();
                }
            }
            else
            {
                SmokeRunning.Stop();
            }

        }
        if (Input.GetKeyUp("left shift") == true)
        {
            PlayerRunOff();
            SmokeRunning.Stop();
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
    private void Start()
    {
        HealthPoint = PlayerPrefs.GetInt("PlayerHP", 100);//update HP
        PlayerHPBarScript.UpdatePlayerHPBar(HealthPoint); //Update PlayerHP to HPBar
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
        //CreateBullet();
        CreateBouncingBullet();
    }
    void CreateBullet()
    {
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();
        float BulletSpeed = 5f;

        if (PlayerSpriteRenderer.flipX == true)
        {
            BulletPosition = Right.position;
            BulletDirection = new Vector2 (1, 0); //right
        }
        else
        {
            BulletPosition = Left.position;
            BulletDirection = new Vector2(-1, 0); //left
        }
        var NewBullet = Instantiate(Bullet, BulletPosition, Quaternion.identity,null); //create new Bullet Meat
        var NewBulletRigidbody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigidbody.velocity = BulletDirection * BulletSpeed;
    }
    void CreateBouncingBullet()
    {
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();
        float BulletSpeed = 4f;

        if (PlayerSpriteRenderer.flipX == true)
        {
            BulletPosition = Right.position;
            BulletDirection = new Vector2(1, 1); //right
        }
        else
        {
            BulletPosition = Left.position;
            BulletDirection = new Vector2(-1, 1); //left
        }
        var NewBullet = Instantiate(BouncingBullet, BulletPosition, Quaternion.identity, null); //create new Bullet Meat
        var NewBulletRigidbody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigidbody.velocity = BulletDirection * BulletSpeed; //Move Bullet

        var RotateSpeed = Random.Range(-1f, -10f);
        NewBulletRigidbody.AddTorque(RotateSpeed); // Rotate Bullet
    }
}