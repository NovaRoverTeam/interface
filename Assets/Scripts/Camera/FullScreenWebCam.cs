using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FullScreenWebCam : MonoBehaviour {

    public int cameraNumber;
    private bool cameraExists = false;
    public WebCamTexture webcamTexture;
    public GameObject warning;
    public GameObject PIPwarning;

    public int PIPCameraNumber;
    public RawImage PIPScreen;
    public RawImage ricohflat;
    public WebCamTexture PIPwebcamTexture;
    private bool PIPcameraExists = false;

    // Use this for initialization
    void Start ()
        {
        WebCamDevice[] devices = WebCamTexture.devices;
        PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber");
            if (devices.Length > PIPCameraNumber)
            {
                PIPcameraExists = true;
                warning.SetActive(false);
                PIPwebcamTexture = new WebCamTexture(devices[PIPCameraNumber].name);
                PIPScreen.texture = PIPwebcamTexture;
                PIPScreen.material.mainTexture = PIPwebcamTexture;
                PIPwebcamTexture.Play();
                Debug.Log("webcam should be active now");

                PIPwebcamTexture.Play();
            }
            else
            {
                Debug.Log("webcam not enabled");
                PIPcameraExists = false;
                warning.SetActive(true);
        }


        cameraNumber = PlayerPrefs.GetInt("CameraNumber");
        if (devices.Length > cameraNumber)
        {
            cameraExists = true;
            PIPwarning.SetActive(false);
            webcamTexture = new WebCamTexture(devices[cameraNumber].name, 1280, 720);
            ricohflat.texture = webcamTexture;
            ricohflat.material.mainTexture = webcamTexture;
            webcamTexture.Play();
            Debug.Log("360 cam should be active now in a double fisheye view");
        }
        else
        {
            Debug.Log("360 cam not enabled");
            cameraExists = false;
            PIPwarning.SetActive(true);
        }
    }

    public void BackBtn(string LoadTarget)
    {
        if (cameraExists == true)
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }

        }

        if (PIPcameraExists == true)
        {
            if (PIPwebcamTexture.isPlaying)
            {
                PIPwebcamTexture.Stop();
            }

        }
        SceneManager.LoadScene(LoadTarget);

    }

    public void ExitBtn()
    {
        if (cameraExists == true)
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }
        }

        if (PIPcameraExists == true)
        {
            if (PIPwebcamTexture.isPlaying)
            {
                PIPwebcamTexture.Stop();
            }
        }
        Application.Quit();
    }

    public void CamSwitchBack()
    {
        if (cameraExists == true)
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }

        }

        if (PIPcameraExists == true)
        {
            if (PIPwebcamTexture.isPlaying)
            {
                PIPwebcamTexture.Stop();
            }

        }
        SceneManager.LoadScene("Sphere");
    }

}
