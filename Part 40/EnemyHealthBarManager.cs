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
    }
    
}
