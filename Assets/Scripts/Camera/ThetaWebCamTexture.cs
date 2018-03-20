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
    public WebCamTexture webcamTexture;
    public CanvasGroup disableablesettings;
    private bool cameraExists = false;

    public int PIPCameraNumber;
    public RawImage PIPScreen;
    public WebCamTexture PIPwebcamTexture;
    private bool PIPcameraExists = false;
    public GameObject PIPwarning;
    public GameObject switchButton;
    int webcamOn;

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
        WebCamDevice[] devices = WebCamTexture.devices;
        cameraNumber = PlayerPrefs.GetInt("CameraNumber");
        webcamOn = PlayerPrefs.GetInt("WebcamOn");
        //decide whether or not we need to disable the settings pane and PIP cam
        if (webcamOn == 1)
            // if the webcam view is on go here.
        { m_Dropdown_sidekick.ClearOptions();
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

            //ensure our canvas groups are on
            disableablesettings.alpha = 1f;
            disableablesettings.blocksRaycasts = true;
            Debug.Log("Dropdown generated and made visible");


        }
        else
        // if the webcam is off go this way. Knock off the disableable things and don't bother with dropdown generation
        {
            disableablesettings.alpha = 0f;
            disableablesettings.blocksRaycasts = false;
        }

      
        if (devices.Length > cameraNumber)
        {
            warning.SetActive(false);
            webcamTexture = new WebCamTexture(devices[cameraNumber].name, 1280, 720);

            sphere1.GetComponent<Renderer>().material.mainTexture = webcamTexture;
            sphere2.GetComponent<Renderer>().material.mainTexture = webcamTexture;

            webcamTexture.Play();
            cameraExists = true;

        }
        else
        {
            Debug.Log("no camera");
            warning.SetActive(true);
            cameraExists = false;
        }

        if (webcamOn == 1)
        {
            PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber");
            if (devices.Length > PIPCameraNumber)
            {
                PIPwarning.SetActive(false);
                PIPwebcamTexture = new WebCamTexture(devices[PIPCameraNumber].name);
                PIPScreen.texture = PIPwebcamTexture;
                PIPScreen.material.mainTexture = PIPwebcamTexture;
                PIPwebcamTexture.Play();
                PIPcameraExists = true;
                Debug.Log("webcam should be active now");
            }
            else
            {
                Debug.Log("no eligible PIP camera");
                PIPcameraExists = false;
                PIPwarning.SetActive(true);
                
            }
        }
        else
        {
            Debug.Log("PIPcam off");
            PIPScreen.enabled = false;
            PIPcameraExists = false;
            PIPwarning.SetActive(false);
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