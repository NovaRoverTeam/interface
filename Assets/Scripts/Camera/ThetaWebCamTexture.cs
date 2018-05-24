using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ThetaWebCamTexture : MonoBehaviour {
    KeyCode backKey = KeyCode.Backspace;
    KeyCode exitKey = KeyCode.Escape;
    KeyCode switchKey = KeyCode.Q;
    KeyCode upKey = KeyCode.UpArrow;
    KeyCode downKey = KeyCode.DownArrow;
    KeyCode enterKey = KeyCode.Return;
    KeyCode clockwise = KeyCode.D;
    KeyCode anticlockwise = KeyCode.A;
    KeyCode space = KeyCode.Space;

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
    float currentWebcamRotation;
    float newWebcamRotation;

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
            webcamTexture = new WebCamTexture(devices[cameraNumber].name);

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
                PlayerPrefs.SetInt("activePIPcamera", PIPCameraNumber);
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

    void Update()
    {
        if (Input.GetKeyDown(exitKey))
        {
            ExitBtn();
        }

        if (Input.GetKeyDown(backKey))
        {
            BackBtn("MainScene");
        }

        if (Input.GetKeyDown(switchKey))
        {
            CamSwitch();
        }

        if (Input.GetKeyDown(clockwise)) 
                { RotateWebcamClockwise(); }

        if (Input.GetKeyDown(anticlockwise))
            { RotateWebcamCounterClockwise(); }

        if (Input.GetKeyDown(space))
            { ToggleWebcam(); }

        // Ensure these commands aren't active when webcam is off
        if (PlayerPrefs.GetInt("WebcamOn") == 1)
        {

            if (Input.GetKeyDown(upKey))
            {
                IncreaseCameraInt();
            }

            if (Input.GetKeyDown(downKey))
            {
                DecreaseCameraInt();
            }
            //only accept the enter key if the new webcam number differs from the current one

            if (PlayerPrefs.GetInt("CameraPIPNumber") != PlayerPrefs.GetInt("activePIPcamera"))
            {
                if (PlayerPrefs.GetInt("CameraPIPNumber") != PlayerPrefs.GetInt("CameraNumber"))
                    {

                        if (Input.GetKeyDown(enterKey))
                        {
                            UpdateWebcamInt();
                        }
                    }
            }

        }
    }

    public void ToggleWebcam()
    { if (PlayerPrefs.GetInt("WebcamOn") == 1)

        {
            
                if (PIPwebcamTexture.isPlaying)
                {
                    PIPwebcamTexture.Stop();
                    PIPScreen.enabled = false;
                }

            
            PlayerPrefs.SetInt("WebcamOn", 0);

        }
        else
            //cheap by calling a sneaky function that has another role
            PIPScreen.enabled = true;
            UpdateWebcamInt();
            PlayerPrefs.SetInt("WebcamOn", 1);
                }


    public void DecreaseCameraInt()
    {
        PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber") - 1;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (0 <= PIPCameraNumber)
        {
            PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
            //Debug.Log("Webcam set to " + devices[PIPCameraNumber].name.ToString());
            m_Dropdown_sidekick.captionText.text = devices[PIPCameraNumber].name.ToString();
        }
        else
        {
            PIPCameraNumber = devices.Length - 1;
            //Debug.Log("Webcam set to " + devices[PIPCameraNumber].name.ToString());
            PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
            m_Dropdown_sidekick.captionText.text = devices[PIPCameraNumber].name.ToString();
        }
        Debug.Log("Total number of devices is " + devices.Length + "Updated webcam number is " + PIPCameraNumber);

    }

    public void IncreaseCameraInt()
    {
        PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber") + 1;
        WebCamDevice[] devices = WebCamTexture.devices;
        if (devices.Length > PIPCameraNumber)
        {
            PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
            //Debug.Log("Webcam set to " + devices[PIPCameraNumber].name.ToString());
            m_Dropdown_sidekick.captionText.text = devices[PIPCameraNumber].name.ToString();
        }
        else
        {
            PIPCameraNumber = 0;
            //Debug.Log("Webcam set to " + devices[PIPCameraNumber].name.ToString());
            PlayerPrefs.SetInt("CameraPIPNumber", PIPCameraNumber);
            m_Dropdown_sidekick.captionText.text = devices[PIPCameraNumber].name.ToString();
        }
        Debug.Log("Total number of devices is " + devices.Length + "Updated webcam number is " + PIPCameraNumber);
    }

    public void UpdateWebcamInt()
    { 
        if (PIPwebcamTexture.isPlaying)
        {
            PIPwebcamTexture.Stop();
        }
        PIPCameraNumber = PlayerPrefs.GetInt("CameraPIPNumber");

        WebCamDevice[] devices = WebCamTexture.devices;
        PIPwarning.SetActive(false);
        PIPwebcamTexture = new WebCamTexture(devices[PIPCameraNumber].name);
        PIPScreen.texture = PIPwebcamTexture;
        PIPScreen.material.mainTexture = PIPwebcamTexture;
        PIPwebcamTexture.Play();
        PIPcameraExists = true;
        PlayerPrefs.SetInt("activePIPcamera", PIPCameraNumber);
        Debug.Log("webcam change complete");
        
    }

    public void RotateWebcamClockwise()
    {
        currentWebcamRotation = PIPScreen.transform.eulerAngles.z;
        newWebcamRotation = currentWebcamRotation + 90;
        if (newWebcamRotation >= 360)
        {
            newWebcamRotation = 0;
        }
        PIPScreen.transform.eulerAngles = new Vector3(0, 0, newWebcamRotation);
    }

    

    public void RotateWebcamCounterClockwise()
    {
        currentWebcamRotation = PIPScreen.transform.eulerAngles.z;
        newWebcamRotation = currentWebcamRotation - 90;
        if (newWebcamRotation < 0)
        {
            newWebcamRotation = 270;
        }
        PIPScreen.transform.eulerAngles = new Vector3(0, 0, newWebcamRotation);
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