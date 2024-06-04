using UnityEngine;

public class TrapOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Trap hit Player");
            PlayerController PlayerControllerScript = collision.gameObject.GetComponent<PlayerController>();
            PlayerControllerScript.PlayerHealthPointUpdate(-1);
        }
    }
    
}
