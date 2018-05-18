/*
 * Define a turtle pose message. This has been hand-crafted from the corresponding
 * turtle message file.
 * 
 * Version History
 * 3.1 - changed methods to start with an upper case letter to be more consistent with c#
 * style.
 * 3.0 - modification from hand crafted version 2.0
 * 
 */
using System.Collections;
using System.Text;
using SimpleJSON;

namespace ROSBridgeLib {
	namespace voltage_msgs {
		public class VoltageMsg : ROSBridgeMsg {
			private float _v1, _v2, _v3, _v4;

			public VoltageMsg(JSONNode msg) {
                //Debug.Log (msg.ToString ());
                _v1 = float.Parse(msg["v1"]["data"]);
                _v2 = float.Parse(msg["v2"]["data"]);
                _v3 = float.Parse(msg["v3"]["data"]);
                _v4 = float.Parse(msg["v4"]["data"]);
			}

			public VoltageMsg(float v1, float v2, float v3, float v4) {
                _v1 = v1;
                _v2 = v2;
                _v3 = v3;
                _v4 = v4;
			}

			public static string getMessageType() {
				return "rover/Voltages";
			}

			public float GetV1() {
				return _v1;
			}

            public float GetV2()
            {
                return _v2;
            }

            public float GetV3()
            {
                return _v3;
            }

            public float GetV4()
            {
                return _v4;
            }

			public override string ToString() {
				return "rover/Voltages [v1=" + _v1 + ",  v2=" + _v2 + ",  v3=" + _v3 + ",  v4=" + _v4 + "]";
			}

			public override string ToYAMLString() {
				return "{\"v1\": " + _v1 + ", \"v2\": " + _v2 + ", \"v3\": " + _v3 + ", \"v4\": " + _v4 + "}";
			}
		}
	}
}
