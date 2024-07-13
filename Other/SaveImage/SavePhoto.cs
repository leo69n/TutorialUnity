using UnityEngine;

public class SavePhoto : MonoBehaviour
{
    private string FinalPath;

    private NativeGallery.Permission permissionGal;

	public string PickPhotoGallery()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            Debug.Log("Image path: " + path);
            if (path != null)
            {
                FinalPath = path; // Picked File path
                
            }
        });

        return FinalPath;
    }
	
    public void SavePhotoToCameraRoll(Texture2D MyTexture, string AlbumName, string filename)
    {
        NativeGallery.SaveImageToGallery(MyTexture, AlbumName, filename, (callback, path) =>
        {
            if (callback == false)
            {
                Debug.Log("Failed to save !");
            }
            else
            {
                Debug.Log("Photo is saved to Camera Roll on phone device.");
            }

        });

    }



    public Texture2D GetTexture2DIOS(string path)
    {
        //Get Texture2D from path of an Image : all types JPG,JPEG,PNG,HEIC...
        Texture2D newText_ = NativeGallery.LoadImageAtPath(path, -1, true, true, false);
        return newText_;
    }


    public async void AskPermissionGal()
    {
        NativeGallery.Permission permissionResultGal = await NativeGallery.RequestPermissionAsync(NativeGallery.PermissionType.Read, NativeGallery.MediaType.Image);
        permissionGal = permissionResultGal;

        if (permissionGal == NativeGallery.Permission.Granted)
        {
            PickPhotoGallery();
        }
    }
    

}
