using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FallBackWebcam : MonoBehaviour {

    public int webCamNumber;
    public int webcamOn;
    int tertiaryCamNumber;
    KeyCode switchKey = KeyCode.Q;
    KeyCode backKey = KeyCode.Backspace;
    KeyCode exitKey = KeyCode.Escape;
    KeyCode upKey = KeyCode.UpArrow;
    KeyCode downKey = KeyCode.DownArrow;
    KeyCode enterKey = KeyCode.Return;
    KeyCode clockwise = KeyCode.RightArrow;
    KeyCode anticlockwise = KeyCode.LeftArrow;
    KeyCode space = KeyCode.Space;
    KeyCode left = KeyCode.L;
    KeyCode right = KeyCode.R;
    KeyCode centre = KeyCode.C;
    private bool cameraExists = false;
    public WebCamTexture webcamTexture;
    public WebCamTexture tertiaryWebcamTexture;
    public GameObject warning;
    public RawImage fallBackScreen;
    public RawImage tertiaryScreen;
    bool tertiaryCamExists;


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
        webcamOn = PlayerPrefs.GetInt("WebcamOn");
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


            webCamNumber = PlayerPrefs.GetInt("CameraPIPNumber");
            if (devices.Length > webCamNumber)
            {
                cameraExists = true;
                warning.SetActive(false);
                webcamTexture = new WebCamTexture(devices[webCamNumber].name);
                fallBackScreen.texture = webcamTexture;
                fallBackScreen.material.mainTexture = webcamTexture;
                webcamTexture.Play();
                Debug.Log("webcam should be active now");
            }
            else
            {
                Debug.Log("webcam not enabled");
                cameraExists = false;
                warning.SetActive(true);
            }
        tertiaryCamNumber = PlayerPrefs.GetInt("CameraTertiary");
        if (devices.Length > tertiaryCamNumber)
        {
            tertiaryWebcamTexture = new WebCamTexture(devices[tertiaryCamNumber].name);
            tertiaryScreen.texture = tertiaryWebcamTexture;
            tertiaryScreen.material.mainTexture = tertiaryWebcamTexture;
            tertiaryWebcamTexture.Play();
            tertiaryCamExists = true;
            Debug.Log("webcam should be active now");
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            CamSwitchBack();
        }

        if (Input.GetKeyDown(exitKey))
        {
            ExitBtn();
        }

        if (Input.GetKeyDown(backKey))
        {
            BackBtn("Sphere");
        }

        if (Input.GetKeyDown(space))
        {
            ToggleWebcam();
        }

        if (tertiaryCamExists == true)
        {
            if (Input.GetKeyDown(left))
            {
                MoveWebcamToLeft();
            }

            if (Input.GetKeyDown(right))
            {
                MoveWebcamToRight();
            }

            if (Input.GetKeyDown(centre))
            {
                MoveWebcamToCentre();
            }
        }
    }

public void BackBtn(string LoadTarget)
    {
        if (cameraExists == true)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
            }

        }

        if (tertiaryCamExists == true)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
            }

        }
        SceneManager.LoadScene(LoadTarget);

    }

    public void ExitBtn()
    {
        if (cameraExists == true)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
            }

        }

        if (tertiaryCamExists == true)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
            }

        }
        Application.Quit();
    }

    public void DropdownChange()
    {
        if (cameraExists == true)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
            }

        }
        tertiaryCamNumber = m_Dropdown_sidekick.value;
        PlayerPrefs.SetInt("CameraPIPNumber", tertiaryCamNumber);
        Debug.Log("Webcam set to " + tertiaryCamNumber.ToString());
        WebCamDevice[] devices = WebCamTexture.devices;

        if (m_Dropdown_sidekick.value == 0)
        { m_Dropdown_sidekick.captionText.text = devices[0].name; }

        webcamTexture = new WebCamTexture(devices[webCamNumber].name);
        webcamTexture.Play();
        Debug.Log("webcam should be active now");

    }

    public void CamSwitchBack()
    {
        if (cameraExists == true)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
            }

        }

        if (tertiaryCamExists == true)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
            }

        }
        SceneManager.LoadScene("Sphere");
    }

    public void ToggleWebcam()
    {
        Debug.Log("Webcam toggled");
        webcamOn = PlayerPrefs.GetInt("WebcamOn");
        Debug.Log(webcamOn.ToString());
        if (webcamOn == 1)
        {
            if (tertiaryWebcamTexture.isPlaying)
            {
                tertiaryWebcamTexture.Stop();
                tertiaryCamExists = false;
            }
            tertiaryScreen.enabled = false;
            Debug.Log("PIP screen should have vanished");

            PlayerPrefs.SetInt("WebcamOn", 0);

        }
        else if (webcamOn == 0)
        {
            //be cheap by calling a sneaky function that has another role
            tertiaryScreen.enabled = true;
            UpdateWebcamInt();
            tertiaryCamExists = true;
            Debug.Log("PIP screen should reappear");
            PlayerPrefs.SetInt("WebcamOn", 1);
        }
    }

    public void UpdateWebcamInt()
    {
        if (tertiaryWebcamTexture.isPlaying)
        {
            tertiaryWebcamTexture.Stop();
        }
        tertiaryCamNumber = PlayerPrefs.GetInt("CameraTertiary");

        WebCamDevice[] devices = WebCamTexture.devices;
        tertiaryWebcamTexture = new WebCamTexture(devices[tertiaryCamNumber].name);
        tertiaryScreen.texture = tertiaryWebcamTexture;
        tertiaryScreen.material.mainTexture = tertiaryWebcamTexture;
        tertiaryWebcamTexture.Play();
        Debug.Log("webcam change complete");
    }

    public void MoveWebcamToLeft()
    {
        tertiaryScreen.transform.position = new Vector3(-400, -200, -1);
    }

    public void MoveWebcamToRight()
    {
        tertiaryScreen.transform.position = new Vector3(150, -200, -1);
    }

    public void MoveWebcamToCentre()
    {
        tertiaryScreen.transform.position = new Vector3(-100, -200, -1);
    }
}
