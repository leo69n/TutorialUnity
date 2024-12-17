using TMPro;
using UnityEngine;

public class PickItemsManager : MonoBehaviour
{
    public GameObject VFXCollected;
    public TextMeshProUGUI ScoreNumber;
    int ScoreTotal = 0;
    public int ScoreOfFruit = 1;
    private void Start()
    {
        UpdateScore(0);
    }
    public void UpdateScore(int AddValue)
    {
        ScoreTotal = ScoreTotal + AddValue; // update ScoreTotal
        ScoreNumber.text = ScoreTotal.ToString(); //update Score Text
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pickable")
        {
            UpdateScore(ScoreOfFruit);

            Instantiate(VFXCollected, collision.gameObject.transform.position,Quaternion.identity,null); //create VFX collect
            Destroy(collision.gameObject); //destroy fruits
        }
    }
}
