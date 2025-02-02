using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Animator AnimatorComponent;
    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Got new Check Point");
            AnimatorComponent.Play("CheckPoint Animation");

            string NewPlayerPosition = this.transform.position.ToString();
            NewPlayerPosition = NewPlayerPosition.Replace("(",""); //remove (
            NewPlayerPosition = NewPlayerPosition.Replace(")",""); //remove )
            NewPlayerPosition = NewPlayerPosition.Replace(" ",""); //remove White space

            PlayerPrefs.SetString("PlayerPosition", NewPlayerPosition);
        }
    }
}
