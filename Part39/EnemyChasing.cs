using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChasing : MonoBehaviour
{
    SpriteRenderer SpriteRendererComponent;
    Rigidbody2D Rigidbody2DComponent;
    GameObject PlayerObject;
    public float MoveSpeed = 1;
    private void Awake()
    {
        SpriteRendererComponent = GetComponent<SpriteRenderer>();
        Rigidbody2DComponent = GetComponent<Rigidbody2D>();
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        StartCoroutine(ChasePlayer());
    }
    IEnumerator ChasePlayer()
    {
        Vector2 DirectionToPlayer = Direction2Points2D(this.transform.position, PlayerObject.transform.position);
        Rigidbody2DComponent.velocity = DirectionToPlayer * MoveSpeed; // start to move to Player
        RotateToPlayer(DirectionToPlayer);

        yield return new WaitForSeconds(1); //Recheck position of Player every 1 second
        StartCoroutine(ChasePlayer());
    }
    void RotateToPlayer(Vector2 DirectionToPlayer)
    {
        if (DirectionToPlayer.x < 0) // =-1: move to Left
        {
            SpriteRendererComponent.flipX = false;
        }
        else // =1: move to Right
        {
            SpriteRendererComponent.flipX = true;
        }
    }
    Vector2 Direction2Points2D(Vector2 Point1, Vector2 Point2) // Start from Point1
    {
        var Direction2D = Point2 - Point1;
        return Direction2D.normalized;
    }
}
