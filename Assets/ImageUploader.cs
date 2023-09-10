using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;

public class ImageUploader : MonoBehaviour
{
    public Image imageComponent;

    public void SelectImage()
    {
        string imagePath = "";

#if UNITY_ANDROID && !UNITY_EDITOR
        // On Android, use AndroidJavaClass to open the image picker.
        AndroidJavaClass unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject pickerIntent = new AndroidJavaObject("android.content.Intent", "android.intent.action.GET_CONTENT");
        pickerIntent.Call<AndroidJavaObject>("setType", "image/*");
        AndroidJavaClass intentClass = new AndroidJavaClass("android.content.Intent");
        AndroidJavaObject chooserIntent = intentClass.CallStatic<AndroidJavaObject>("createChooser", pickerIntent, "Select Image");
        currentActivity.Call("startActivityForResult", chooserIntent, 0);
#else
        // For Unity Editor or other platforms, you'll need to implement custom file selection logic.
        imagePath = UnityEditor.EditorUtility.OpenFilePanel("Select Image", "", "png,jpg,jpeg");
#endif

        if (!string.IsNullOrEmpty(imagePath))
        {
            // Process the image
            byte[] imageData = File.ReadAllBytes(imagePath);
            Texture2D selectedTexture = new Texture2D(2, 2);
            selectedTexture.LoadImage(imageData);

            // Display the image on the UI
            imageComponent.sprite = Sprite.Create(selectedTexture, new Rect(0, 0, selectedTexture.width, selectedTexture.height), new Vector2(0.5f, 0.5f));

            // Upload the image to the server using a coroutine
            StartCoroutine(UploadImageToServer(imageData));
        }
    }

    private System.Collections.IEnumerator UploadImageToServer(byte[] imageData)
    {
        // Replace 'YOUR_UPLOAD_URL' with your actual server URL for image uploading
        string uploadUrl = "https://drive.google.com/drive/folders/1f1RSlV4U8z_GulelhcR0nlaFwOHsTDoc?usp=sharing";

        // Create a new UnityWebRequest to upload the image data
        UnityWebRequest www = UnityWebRequest.Put(uploadUrl, imageData);
        www.method = "POST";
        www.SetRequestHeader("Content-Type", "image/jpeg"); // Modify content type as needed

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Image upload successful!");
            // Handle the server response if needed
        }
        else
        {
            Debug.LogError("Image upload failed: " + www.error);
        }
    }
}