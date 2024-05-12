using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 VectorToRight = new Vector2(1, 0);
    Vector2 VectorToLeft = new Vector2(-1, 0);

    void Update()
    {
        if (Input.GetKey("d"))
        {
            GetComponent<Rigidbody2D>().velocity = VectorToRight;
        }

        if (Input.GetKey("a"))
        {
            GetComponent<Rigidbody2D>().velocity = VectorToLeft;
        }
    }
}
