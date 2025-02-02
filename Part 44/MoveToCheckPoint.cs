using UnityEngine;

public class MoveToCheckPoint : MonoBehaviour
{
    void Start()
    {
        // Move Player to CheckPoint location when Game is started
        string CurrentCheckPointLocation = PlayerPrefs.GetString("PlayerPosition", "null");
        if (CurrentCheckPointLocation != "null")
        {
            string[] LocationXYZ = CurrentCheckPointLocation.Split(",");
            float LocationX = float.Parse(LocationXYZ[0]); // get X of CheckPoint Location
            float LocationY = float.Parse(LocationXYZ[1]); // get Y of CheckPoint Location
            float LocationZ = float.Parse(LocationXYZ[2]); // get Z of CheckPoint Location

            this.transform.position = new Vector3(LocationX, LocationY, LocationZ); // move to current Check Point location
        }

    }

}
