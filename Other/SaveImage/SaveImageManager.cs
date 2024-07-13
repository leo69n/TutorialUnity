using UnityEngine;
using UnityEngine.UI;

public class SaveImageManager : MonoBehaviour
{
    public SavePhoto SavePhotoComponent;
    public RawImage SavingImage;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            string PickedPath = SavePhotoComponent.PickPhotoGallery();
            Texture2D PickedTexture2D = SavePhotoComponent.GetTexture2DIOS(PickedPath);
            SavePhotoComponent.SavePhotoToCameraRoll(PickedTexture2D,"random", "saved_photo");
        }
    }
}
