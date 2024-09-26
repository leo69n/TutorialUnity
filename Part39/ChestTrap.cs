using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrap : MonoBehaviour
{
    public GameObject Enemy;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CreateEnemy();
        }
    }
    void CreateEnemy()
    {
        Instantiate(Enemy,this.transform.position,Quaternion.identity,null);
    }
}
