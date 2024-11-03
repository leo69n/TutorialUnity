using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float Damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthBarManager>().UpdateHealth(-1 * Damage);
        }
    }
}
