using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Animator AnimatorComponent;
    SpriteRenderer SpriteRendererComponent;
    Rigidbody2D Rigidbody2DComponent;
    public List<GameObject> Paths;
    int CurrentIndex = 0; //index of current Path
    GameObject CurrentPath;
    public float MoveSpeed = 1;
    public float WaitTime = 1;
    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
        SpriteRendererComponent = GetComponent<SpriteRenderer>();
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
        CurrentPath = Paths[CurrentIndex];
    }
    void ChangePlatformDirectionMovement()
    {
        CurrentIndex = CurrentIndex + 1;
        if (CurrentIndex >= Paths.Count)
        {
            CurrentIndex = 0;
        }
        CurrentPath = Paths[CurrentIndex];
        StartCoroutine(MoveTo());
    }
    void Start()
    {
        StartCoroutine(MoveTo());
    }
    IEnumerator MoveTo()
    {
        Rigidbody2DComponent.velocity = Vector2.zero; // stop and waiting
        AnimatorComponent.Play("Enemy Idle");
        yield return new WaitForSeconds(WaitTime);

        var DirectionToPath = Direction2Points2D(transform.position, CurrentPath.transform.position);
        Rigidbody2DComponent.velocity = DirectionToPath * MoveSpeed; // moving to path

        RotateToPath(DirectionToPath);
        AnimatorComponent.Play("Enemy Walk");
    }
    void RotateToPath(Vector2 DirectionToPath)
    {
        if (DirectionToPath.x < 0) // = -1 : moving to Left
        {
            SpriteRendererComponent.flipX = false; 
        }
        else // = 1 : moving to Right
        {
            SpriteRendererComponent.flipX = true;
        }
    }
    Vector2 Direction2Points2D(Vector2 Point1, Vector2 Point2) // Start from Point1
    {
        var Direction2D = Point2 - Point1;
        return Direction2D.normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == CurrentPath)
        {
            ChangePlatformDirectionMovement();
        }
    }
}
