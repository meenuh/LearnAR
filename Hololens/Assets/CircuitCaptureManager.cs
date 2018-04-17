using UnityEngine;
using UnityEngine.XR.WSA.WebCam;
using HoloToolkit.Unity.InputModule;

public class CircuitCaptureManager : MonoBehaviour {
    static public int imageCount = 0;

    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    GameObject displayObject = null;
    [SerializeField]
    DisplayManager dsp = null;
    

    // Use this for initialization
    void Start () {
        //displayObject = new DisplayManager();
        //displayObject = GameObject.FindGameObjectWithTag("CaptureManager");
    }

    public void CaptureCircuitAndSave()
    {
        //Resolution cameraResolution = PhotoCapture.SupportedResolutions;
        Debug.Log("CaptureCircuitAndSave");
        PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
            photoCaptureObject = captureObject;
            CameraParameters cameraParameters = new CameraParameters();
            cameraParameters.hologramOpacity = 0.0f;
            cameraParameters.cameraResolutionWidth = 896;
            cameraParameters.cameraResolutionHeight = 504;
            cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

            // Activate the camera
            photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
                string filename = string.Format(@"CircuitImage{0}.jpg", imageCount);
                if(!System.IO.File.Exists(Application.persistentDataPath))
                {
                    System.IO.File.Create(Application.persistentDataPath);
                }
                string filePath = System.IO.Path.Combine(Application.persistentDataPath, filename);

                imageCount++;
                // Take a picture
                photoCaptureObject.TakePhotoAsync(filePath, PhotoCaptureFileOutputFormat.JPG, OnCapturedPhotoToDisk);
                //OnCapturedPhotoToDisk(result);
            });
        });
    }

    void OnCapturedPhotoToDisk(PhotoCapture.PhotoCaptureResult result)
    {
        //photoCaptureFrame.UploadImageDataToTexture(targetTexture);
        //byte[] rawData = targetTexture.GetRawTextureData();

        //send via http here
        Debug.Log("OnCapturedPhotoToDisk");
        //if (displayObject.GetComponent<DisplayManager>() != null)
        //{
        //dsp = displayObject.GetComponent<DisplayManager>();
        dsp.DisplayCircuit(imageCount - 1);
        //}
        //else if (dsp != null)
        //{
        //    dsp.DisplayCircuit(imageCount - 1);
        //}
        //possibly display image here?
        //displayObject.DisplayCircuit(imageCount - 1);
        photoCaptureObject.StopPhotoModeAsync(OnStoppedPhotoMode);
    }

    void Update () {
		
	}

    void OnStoppedPhotoMode(PhotoCapture.PhotoCaptureResult result)
    {
        photoCaptureObject.Dispose();
        photoCaptureObject = null;
    }
}
