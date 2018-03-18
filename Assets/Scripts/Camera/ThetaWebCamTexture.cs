using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject switchButton;
    public GameObject toggleUpButton;
    public GameObject toggleDownButton;

    //Dropdown Objects for PIP cam
    public Dropdown m_Dropdown_sidekick;
    string m_MyString_sidekick;
    int m_Index_sidekick;

    //Drop down options:
    //Use these for adding options to the Dropdown List
    Dropdown.OptionData m_NewData;
    //The list of messages for the Dropdown
    List<Dropdown.OptionData> m_Messages = new List<Dropdown.OptionData>();

    void Start() 
	{
        m_Dropdown_sidekick.ClearOptions();
        WebCamDevice[] devices = WebCamTexture.devices;

        //populate a message with all of the USB webcam devices
        for (int i = 0; i < devices.Length; i++)
        {
            m_NewData = new Dropdown.OptionData();
            m_NewData.text = devices[i].name;
            //Debug.Log("Following added to Dropdown list " + m_NewData.text);
            m_Messages.Add(m_NewData);
        }

        //Take each entry in the message List to generate both menus
        foreach (Dropdown.OptionData message in m_Messages)
        {
            m_Dropdown_sidekick.options.Add(message);
            m_Index_sidekick = m_Messages.Count - 1;
        }
        if (m_Dropdown_sidekick.value == 0)
        { m_Dropdown_sidekick.captionText.text = devices[0].name; }

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
                PIPwebcamTexture = new WebCamTexture(devices[PIPCameraNumber].name);
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
                m_Dropdown_sidekick.Hide();
                PIPwarning.SetActive(false);
                switchButton.SetActive(false);
                toggleUpButton.SetActive(false);
                toggleDownButton.SetActive(false);
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


    public void CamScrollUp()
    {
        if (PIPcameraExists == true)
        {
            if (PIPwebcamTexture.isPlaying)
            {
                PIPwebcamTexture.Stop();
            }

        }

        PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber") + 1;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > PIPCameraNumber)
        {
            PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
            Debug.Log("Webcam set to " + PIPCameraNumber.ToString());
        }
        else
        PIPCameraNumber = 0;
        Debug.Log("Webcam set to " + PIPCameraNumber.ToString());
        PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
        PIPwebcamTexture = new WebCamTexture(devices[PIPCameraNumber].name);
        PIPScreen.texture = PIPwebcamTexture;
        PIPScreen.material.mainTexture = PIPwebcamTexture;
        PIPwebcamTexture.Play();
        Debug.Log("webcam should be active now");

        PIPwebcamTexture.Play();

    }

    public void CamScrollDown()
    {
        if (PIPcameraExists == true)
        {
            if (PIPwebcamTexture.isPlaying)
            {
                PIPwebcamTexture.Stop();
            }

        }

        PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber") - 1;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (0 <= PIPCameraNumber)
        {
            PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
            Debug.Log("Webcam set to " + PIPCameraNumber.ToString());
        }
        else
        PIPCameraNumber = devices.Length - 1;
        Debug.Log("Webcam set to " + PIPCameraNumber.ToString());
        PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
        PIPwebcamTexture = new WebCamTexture(devices[PIPCameraNumber].name);
        PIPScreen.texture = PIPwebcamTexture;
        PIPScreen.material.mainTexture = PIPwebcamTexture;
        PIPwebcamTexture.Play();
        Debug.Log("webcam should be active now");

        PIPwebcamTexture.Play();

    }


    public void DropdownChange()
    {
        if (PIPcameraExists == true)
        {
            if (PIPwebcamTexture.isPlaying)
            {
                PIPwebcamTexture.Stop();
            }

        }
        PIPCameraNumber = m_Dropdown_sidekick.value;
        PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
        Debug.Log("Webcam set to " + PIPCameraNumber.ToString());
        WebCamDevice[] devices = WebCamTexture.devices;

        if (m_Dropdown_sidekick.value == 0)
        { m_Dropdown_sidekick.captionText.text = devices[0].name; }

        PIPwebcamTexture = new WebCamTexture(devices[PIPCameraNumber].name);
        PIPScreen.texture = PIPwebcamTexture;
        PIPScreen.material.mainTexture = PIPwebcamTexture;
        PIPwebcamTexture.Play();
        Debug.Log("webcam should be active now");

    }
}