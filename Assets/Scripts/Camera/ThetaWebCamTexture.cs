using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThetaWebCamTexture : MonoBehaviour {

    public int cameraNumber;
    public GameObject sphere1;
    public GameObject sphere2;
    public GameObject warning;
    private bool cameraExists = false;
    public WebCamTexture webcamTexture;

    public int PIPCameraNumber;
    public RawImage PIPScreen;
    public WebCamTexture PIPwebcamTexture;
    private bool PIPcameraExists = false;
    public GameObject PIPwarning;
	
	void Start() 
	{
		WebCamDevice[] devices = WebCamTexture.devices;
        cameraNumber = PlayerPrefs.GetInt("CameraNumber");
        if (devices.Length > cameraNumber)
        {
            cameraExists = true;
            warning.SetActive(false);
            webcamTexture = new WebCamTexture(devices[cameraNumber].name, 1280, 720);

            sphere1.GetComponent<Renderer>().material.mainTexture = webcamTexture;
            sphere2.GetComponent<Renderer>().material.mainTexture = webcamTexture;

            webcamTexture.Play();
        }
        else
        {
            Debug.Log("no camera");
            cameraExists = false;
            warning.SetActive(true);
        }

        if (PlayerPrefs.GetInt("WebcamOn") == 1)
        {
            PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber");
            if (devices.Length > PIPCameraNumber)
            {
                PIPcameraExists = true;
                PIPwarning.SetActive(false);
                PIPwebcamTexture = new WebCamTexture();
                PIPScreen.texture = PIPwebcamTexture;
                PIPScreen.material.mainTexture = PIPwebcamTexture;
                PIPwebcamTexture.Play();
                Debug.Log("webcam should be active now");

                PIPwebcamTexture.Play();
            }
            else
            {
                Debug.Log("no camera");
                PIPcameraExists = false;
                PIPwarning.SetActive(true);
            }
        }
        else
        {       PIPcameraExists = false;
                PIPwarning.SetActive(false);
                PIPScreen.enabled = false;
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

    public void ExitBtn(string LoadTarget)
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

    public void CamSwitch()
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
        SceneManager.LoadScene("WebcamPlane");
    }
}