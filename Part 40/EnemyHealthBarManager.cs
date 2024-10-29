using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    public GameObject HealthBarPrefab;
    public GameObject HealthBarLocation;
    public float EnemyCurrentHealth;
    public float EnemyMaxHealth;
    HealthBarManager HealthBarManagerComponent;
    private void Awake()
    {
        CreateHealthBar();
    }
    void CreateHealthBar()
    {
        var HealthBarObject = Instantiate(HealthBarPrefab, HealthBarLocation.transform.position,Quaternion.identity,this.transform);
        HealthBarManagerComponent = HealthBarObject.GetComponent<HealthBarManager>();
        HealthBarManagerComponent.SetHealth(EnemyCurrentHealth, EnemyMaxHealth); //update Health Bar
    }
    public void UpdateHealth(float number)
    {
        EnemyCurrentHealth = EnemyCurrentHealth + number;
        if (EnemyCurrentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
        HealthBarManagerComponent.SetHealth(EnemyCurrentHealth, EnemyMaxHealth); //update Health Bar
    }
}
