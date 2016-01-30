using UnityEngine;
using System.Collections;

static public class LayerManagementVectorExtensions {

	static public bool LayerManagementisEqualTo (this Vector2 v1, Vector2 v2) {
		return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y);
	}
	
	static public bool LayerManagementisEqualTo (this Vector3 v1, Vector3 v2) {
		return Mathf.Approximately(v1.x, v2.x) && Mathf.Approximately(v1.y, v2.y) && Mathf.Approximately(v1.z, v2.z);
	}

	static public Vector2 LayerManagementSetX (this Vector2 v, float x) {
		v.x = x;
		return v;
	}

	static public Vector2 LayerManagementSetY (this Vector2 v, float y) {
		v.y = y;
		return v;
	}

	static public Vector3 LayerManagementSetX (this Vector3 v, float x) {
		v.x = x;
		return v;
	}

	static public Vector3 LayerManagementSetY (this Vector3 v, float y) {
		v.y = y;
		return v;
	}

	static public Vector3 LayerManagementSetZ (this Vector3 v, float z) {
		v.z = z;
		return v;
	}

	static public Vector2 LayerManagementToVector2 (this Vector3 v) {
		return new Vector2 (v.x, v.y);
	}

	static public Vector3 LayerManagementToVector3 (this Vector2 v, float z) {
		return new Vector3 (v.x, v.y, z);
	}

	static public string LayerManagementToStringOverload (this Vector3 v) {
		return "("+v.x+", "+v.y+", "+v.z+")";
	}
	
	static public string LayerManagementToStringOverload (this Vector2 v) {
		return "("+v.x+", "+v.y+")";
	}

}
