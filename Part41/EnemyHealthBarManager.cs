using TMPro;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    GameObject DamageText;
    public GameObject HealthBarPrefab;
    public GameObject HealthBarLocation;
    public GameObject DieEffect;
    public float EnemyCurrentHealth;
    public float EnemyMaxHealth;
    HealthBarManager HealthBarManagerComponent;
    private void Awake()
    {
        CreateHealthBar();
        DamageText = Resources.Load<GameObject>("Prefab/Damage Text");
    }
    void CreateHealthBar()
    {
        var HealthBarObject = Instantiate(HealthBarPrefab, HealthBarLocation.transform.position,Quaternion.identity,this.transform);
        HealthBarManagerComponent = HealthBarObject.GetComponent<HealthBarManager>();
        HealthBarManagerComponent.SetHealth(EnemyCurrentHealth, EnemyMaxHealth); //update Health Bar
    }
    public void UpdateHealth(float number)
    {
        ShowDamage(number);
        EnemyCurrentHealth = EnemyCurrentHealth + number;
        if (EnemyCurrentHealth <= 0)
        {
            Instantiate(DieEffect, GetComponent<Collider2D>().bounds.center , Quaternion.identity, null);
            Destroy(this.gameObject);
        }
        HealthBarManagerComponent.SetHealth(EnemyCurrentHealth, EnemyMaxHealth); //update Health Bar
    }
    void ShowDamage(float DamageNumber)
    {
        var DamageTextObject = Instantiate(DamageText, HealthBarLocation.transform.position + new Vector3(0,0.5f,0), Quaternion.identity, null);
        DamageTextObject.GetComponent<TextMeshPro>().text = DamageNumber.ToString();
    }
    
}
