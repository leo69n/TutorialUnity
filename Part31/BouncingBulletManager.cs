using UnityEngine;

public class BouncingBulletManager : MonoBehaviour
{
    int TouchCount = 0;
    public GameObject HitEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TouchCount = TouchCount + 1;
        if (TouchCount >= 3)
        {
            Instantiate(HitEffect, transform.position, Quaternion.identity, null); // Create Hit Effect
            Destroy(this.gameObject); // Destroy this Bullet if Bounce 3 times 
        }
    }
}
