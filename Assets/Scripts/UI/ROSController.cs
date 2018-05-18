using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;

public class ROSController : MonoBehaviour  {
	private ROSBridgeWebSocketConnection ros = null;
    public Text voltageText, GPSText, bearingText, destinationBearing;
    public TextMesh distanceToDestination;
    public float outputVoltage, outputBearing, outputDistance, outputDestinationBearing;

	// the critical thing here is to define our subscribers, publishers and service response handlers
	void Start () {
		ros = new ROSBridgeWebSocketConnection ("ws://192.168.1.199", 9090);
		//ros.AddSubscriber(typeof(VoltageRead));
		//ros.AddSubscriber(typeof(GPSRead));
        ros.AddSubscriber(typeof(BearingRead));
        //ros.AddSubscriber(typeof(RetrieveRead));
        ros.Connect();
        PlayerPrefs.SetInt("rosOn", 1);
        Debug.Log("ROS is on");
    }

	// extremely important to disconnect from ROS. OTherwise packets continue to flow
	void OnApplicationQuit() {
		if(ros!=null)
               ros.Disconnect ();
           PlayerPrefs.SetInt("rosOn", 0);
    }

	void Update () {
        PlayerPrefs.SetInt("rosOn", 1);
        //voltageText.text = VoltageRead.voltage4.ToString("0.#") + "V";
        //outputVoltage = VoltageRead.voltage4;
        //GPSText.text = GPSRead.latitude.ToString("0.######") + ", " + GPSRead.longitude.ToString("0.######");
        bearingText.text = BearingRead.bearing.ToString("") + "degrees";
        outputBearing = BearingRead.bearing;
        Debug.Log(BearingRead.bearing);
       // distanceToDestination.text = RetrieveRead.destinationDistance.ToString("") + "m";
        //outputDistance = RetrieveRead.destinationDistance;
        //destinationBearing.text = RetrieveRead.destinationBearing.ToString("") + "degrees";
        //outputDestinationBearing = RetrieveRead.destinationBearing;
        ros.Render ();
    }
}
