using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.voltage_msgs;
using System.Collections;
using SimpleJSON;
using UnityEngine;

/**
 * This is a toy example of the Unity-ROS interface talking to the TurtleSim 
 * tutorial (circa Groovy). Note that due to some changes since then this will have
 * to be slightly re-written, but as its a test ....
 * 
 * This defines the callback that links the pose message. It moves the Dalek with
 * the turtlesim
 * 
 * @author Michael Jenkin, Robert Codd-Downey and Andrew Speers
 * @version 3.0
 **/

public class VoltageRead : ROSBridgeSubscriber {
	public static float voltage1, voltage2, voltage3, voltage4;

	public new static string GetMessageTopic() {
		return "/voltage";
	}  

	public new static string GetMessageType() {
		return "rover/Voltages";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new Float32Msg(msg);
	}

	public new static void CallBack(ROSBridgeMsg msg) {
		VoltageMsg volts = (VoltageMsg) msg;
        voltage1 = volts.GetV1 ();
        voltage2 = volts.GetV2();
        voltage3 = volts.GetV3();
        voltage4 = volts.GetV4();
    }
}