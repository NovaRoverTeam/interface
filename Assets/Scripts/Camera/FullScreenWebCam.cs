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

    //Dropdown Objects for PIP cam
    public Dropdown m_Dropdown_sidekick;
    string m_MyString_sidekick;
    int m_Index_sidekick;

    //Drop down options:
    //Use these for adding options to the Dropdown List
    Dropdown.OptionData m_NewData;
    //The list of messages for the Dropdown
    List<Dropdown.OptionData> m_Messages = new List<Dropdown.OptionData>();

    // Use this for initialization
    void Start ()
        {
        WebCamDevice[] devices = WebCamTexture.devices;
        m_Dropdown_sidekick.ClearOptions();

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
