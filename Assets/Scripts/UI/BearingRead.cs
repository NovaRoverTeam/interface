using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
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

public class BearingRead : ROSBridgeSubscriber {
	public static int bearing;

	public new static string GetMessageTopic() {
		return "/bearing";
	}  

	public new static string GetMessageType() {
		return "std_msgs/Int32";
	}

	public new static ROSBridgeMsg ParseMessage(JSONNode msg) {
		return new Int32Msg(msg);
	}

	public new static void CallBack(ROSBridgeMsg msg) {
		Int32Msg bear = (Int32Msg) msg;
		bearing = bear.GetData ();
	}
}