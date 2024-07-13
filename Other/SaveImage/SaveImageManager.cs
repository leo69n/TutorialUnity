using UnityEngine;

public class SaveImageManager : MonoBehaviour
{
    public SavePhoto SavePhotoComponent;
    public Texture2D Image2D; //drag your Image PNG here
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SavingPhotoToDevice();
        }
        
    }

    public void SavingPhotoToDevice()
    {
        SavePhotoComponent.SavePhotoToCameraRoll(Image2D, "random", "savedphoto.png");
    }
}
