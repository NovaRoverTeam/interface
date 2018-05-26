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
    private bool cameraExists = false;
    public WebCamTexture webcamTexture;
    public WebCamTexture tertiaryWebcamTexture;
    public GameObject warning;
    public RawImage fallBackScreen;
    public RawImage tertiaryScreen;


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
            Debug.Log("webcam should be active now");
        }


    }

    void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            CamSwitchBack();
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
        Application.Quit();
    }

    public void DropdownChange()
    {
        if (cameraExists == true)
        {
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }

        }
        webCamNumber = m_Dropdown_sidekick.value;
        PlayerPrefs.SetInt("CameraPIPNumber", webCamNumber);
        Debug.Log("Webcam set to " + webCamNumber.ToString());
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
            if (webcamTexture.isPlaying)
            {
                webcamTexture.Stop();
            }

        }
        SceneManager.LoadScene("Sphere");
    }

}
