using UnityEngine;
using UnityEngine.XR.WSA.WebCam;
using HoloToolkit.Unity.InputModule;

public class CircuitCaptureManager : MonoBehaviour {
    //static public int imageCount = 0;

    PhotoCapture photoCaptureObject = null;
    Texture2D targetTexture = null;
    GameObject displayObject = null;
    [SerializeField]
    DisplayManager dsp = null;

    private void Start()
    {

    }


    public void CaptureCircuitAndSave()
    {
        PhotoCapture.CreateAsync(false, OnPhotoCaptureCreated);
        //PhotoCapture.CreateAsync(false, delegate (PhotoCapture captureObject) {
        //    photoCaptureObject = captureObject;
        //    CameraParameters cameraParameters = new CameraParameters();
        //    cameraParameters.hologramOpacity = 0.0f;
        //    cameraParameters.cameraResolutionWidth = 1280;
        //    cameraParameters.cameraResolutionHeight = 720;
        //    cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

        //    // Activate the camera
        //    photoCaptureObject.StartPhotoModeAsync(cameraParameters, delegate (PhotoCapture.PhotoCaptureResult result) {
 
        //        // Take a picture
        //        photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);

        //    });
        //});
    }

    void OnPhotoCaptureCreated(PhotoCapture captureObject)
    {
        photoCaptureObject = captureObject;

        CameraParameters cameraParameters = new CameraParameters();
        cameraParameters.hologramOpacity = 0.0f;
        cameraParameters.cameraResolutionWidth = 1280;
        cameraParameters.cameraResolutionHeight = 720;
        cameraParameters.pixelFormat = CapturePixelFormat.BGRA32;

        captureObject.StartPhotoModeAsync(cameraParameters, OnPhotoModeStarted);
    }

    private void OnPhotoModeStarted(PhotoCapture.PhotoCaptureResult result)
    {
        if(result.success)
        {
            photoCaptureObject.TakePhotoAsync(OnCapturedPhotoToMemory);
        }
    }

    void OnCapturedPhotoToMemory(PhotoCapture.PhotoCaptureResult result, PhotoCaptureFrame photoCaptureFrame)
    {
        if(result.success)
        {
            targetTexture = new Texture2D(1280, 720);

            // Copy the raw image data into our target texture
            photoCaptureFrame.UploadImageDataToTexture(targetTexture);
            dsp.DisplayCircuit(targetTexture);
        }

        // Deactivate our camera
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
