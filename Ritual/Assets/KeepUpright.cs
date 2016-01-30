using UnityEngine;
using System.Collections;

public class KeepUpright : MonoBehaviour {

	Transform t, cam;


	// Use this for initialization
	void Start () {
		t = transform;
		cam = GameObject.FindGameObjectWithTag ("MainCamera").transform;
	}
	
	// Update is called once per frame
	void Update () {
		t.localRotation = Quaternion.Euler (new Vector3(-cam.localRotation.eulerAngles.x,0f,0f));
		
	}
}
