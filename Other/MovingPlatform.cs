using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Rigidbody2D Rigidbody2DComponent;
    public float speed;
    /* Moving non-stop
    private bool GoingToRight = false;
    void Start()
    {
        StartCoroutine(MoveStart());
    }
    void Update()
    {
        if (GoingToRight == true)
        {
            Rigidbody2DComponent.velocity = new Vector2(1f * speed, 0);
        }
        else
        {
            Rigidbody2DComponent.velocity = new Vector2(-1f * speed, 0);
        }
    }
    IEnumerator MoveStart()
    {
        yield return new WaitForSeconds(1f);
        if (GoingToRight == false)
        {
            GoingToRight = true;
            
        }
        else
        {
            GoingToRight = false;
            
        }
        StartCoroutine(MoveStart());
    }
    */

    //moving with a stop
    public int GoingToRight = 0;
    void Start()
    {
        StartCoroutine(MoveStart());
    }
    void Update()
    {
        if (GoingToRight == 1)
        {
            Rigidbody2DComponent.velocity = new Vector2(1f * speed, 0);
        }
        else if (GoingToRight == 2)
        {
            Rigidbody2DComponent.velocity = new Vector2(-1f * speed, 0);
        }
        else
        {
            Rigidbody2DComponent.velocity = new Vector2(0f * speed, 0);
        }
    }
    IEnumerator MoveStart()
    {
        yield return new WaitForSeconds(1f);
        if (GoingToRight == 1)
        {
            GoingToRight = -1;
        }
        if (GoingToRight == 2)
        {
            GoingToRight = -2;
        }
        yield return new WaitForSeconds(1f);
        if (GoingToRight == -1)
        {
            GoingToRight = 2;

        }
        else
        {
            GoingToRight = 1;

        }
        StartCoroutine(MoveStart());
    }
}
