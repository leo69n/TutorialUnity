using System.Collections;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject HitEffect;
    void Start()
    {
        StartCoroutine(DestroyTime());
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
        CreateHitEffect();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
        CreateHitEffect();
    }
    void CreateHitEffect()
    {
        Instantiate(HitEffect,transform.position,Quaternion.identity, null);
    }
}
