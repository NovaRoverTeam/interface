using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ThetaWebCamTexture : MonoBehaviour {

    public int cameraNumber;
    public GameObject sphere1;
    public GameObject sphere2;

    public WebCamTexture webcamTexture;
	
	void Start() 
	{
		WebCamDevice[] devices = WebCamTexture.devices;
        cameraNumber = PlayerPrefs.GetInt("CameraNumber");
        if (devices.Length > cameraNumber)
        {
			webcamTexture = new WebCamTexture(devices[cameraNumber].name, 1280, 720);

            sphere1.GetComponent<Renderer>().material.mainTexture = webcamTexture;
            sphere2.GetComponent<Renderer>().material.mainTexture = webcamTexture;

            webcamTexture.Play();
		}
        else
        {
			Debug.Log("no camera");
		}
	}

    public void BackBtn(string LoadTarget)
    {
        if (webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
        SceneManager.LoadScene(LoadTarget);
    }

    public void ExitBtn(string LoadTarget)
    {
        if (webcamTexture.isPlaying)
        {
            webcamTexture.Stop();
        }
        Application.Quit();
    }
}