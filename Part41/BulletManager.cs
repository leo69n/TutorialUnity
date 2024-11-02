using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject HitEffect;
    public float Damage;
    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(3f);
        CreateHitEffect();
        Destroy(this.gameObject);
    }

    void CreateHitEffect()
    {
        Instantiate(HitEffect, transform.position, Quaternion.identity, null);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealthBarManager>().UpdateHealth(-1 * Damage); //Add Damage to Enemy
        }
        CreateHitEffect();
        Destroy(this.gameObject); // Destroy this Bullet
    }
}
