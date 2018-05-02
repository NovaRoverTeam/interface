using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using ROSBridgeLib;
using System.Reflection;
using System;

public class ROSController : MonoBehaviour  {
	private ROSBridgeWebSocketConnection ros = null;
    public Text rosText, GPSText, bearingText, destinationBearing;
    public TextMesh distanceToDestination;
    public float outputVoltage, outputBearing, outputDistance, outputDestinationBearing;

	// the critical thing here is to define our subscribers, publishers and service response handlers
	void Start () {
		ros = new ROSBridgeWebSocketConnection ("ws://192.168.1.199", 9090);
		ros.AddSubscriber (typeof(VoltageRead));
		ros.AddSubscriber (typeof(GPSRead));
        ros.AddSubscriber(typeof(BearingRead));
        ros.AddSubscriber(typeof(DestinationBearingRead));
        ros.AddSubscriber(typeof(DistanceRead));
        ros.Connect ();
        PlayerPrefs.SetInt("rosOn", 1);
    }

	// extremely important to disconnect from ROS. OTherwise packets continue to flow
	void OnApplicationQuit() {
		if(ros!=null)
            ros.Disconnect ();
            PlayerPrefs.SetInt("rosOn", 0);
    }

	void Update () {
		rosText.text = VoltageRead.voltage.ToString("0.#") + "V";
        outputVoltage = VoltageRead.voltage;
        GPSText.text = GPSRead.latitude.ToString("0.######") + ", " + GPSRead.longitude.ToString("0.######");
        bearingText.text = BearingRead.bearing.ToString("") + "degrees";
        outputBearing = BearingRead.bearing;
        distanceToDestination.text = DistanceRead.distance.ToString("") + "m";
        outputDistance = DistanceRead.distance;
        destinationBearing.text = DestinationBearingRead.destinationBearing.ToString("") + "degrees";
        outputDestinationBearing = DestinationBearingRead.destinationBearing;
        ros.Render ();
	}
}
