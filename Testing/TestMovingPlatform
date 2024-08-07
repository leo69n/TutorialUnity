using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public List<GameObject> PathList = new List<GameObject>();
    int CurrentIndex = 0;
    
    Rigidbody2D Rigidbody2DComponent;
    GameObject NextPath;
    PlayerController PlayerScript;
    public float MyVelocityX;
    public float Speed = 3f;
    bool isMoving = false;

    private void Awake()
    {
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
        PlayerScript = FindObjectOfType<PlayerController>();
    }
    void Start()
    {
        StartMovement();
    }

    void FixedUpdate()
    {
        MyVelocityX = Rigidbody2DComponent.velocity.x;
    }
    void StartMovement()
    {
        isMoving = true;
        NextPath = PathList[CurrentIndex];
        var Dir = Direction2Points2D(NextPath.transform.position, transform.position);
        Rigidbody2DComponent.velocity = Dir * Speed;
    }
    Vector2 Direction2Points2D(Vector2 Point1, Vector2 Point2) // Start from Point1
    {
        var Direction2D = Point1 - Point2;
        return Direction2D.normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == NextPath)
        {
            CurrentIndex++;
            if (CurrentIndex >= PathList.Count)
                CurrentIndex = 0;
            StartCoroutine(TakeBreak());
        }
    }
    
    IEnumerator TakeBreak()
    {
        isMoving = false;
        Rigidbody2DComponent.velocity = new Vector2(0,0);

        yield return new WaitForSeconds(2);
        StartMovement();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            PlayerScript.MovingPlatformSpeed = 0;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (isMoving == true)
            {
                PlayerScript.MovingPlatformSpeed = MyVelocityX;
            }
            else
            {
                PlayerScript.MovingPlatformSpeed = 0;
            }
            
        }
    }
}
