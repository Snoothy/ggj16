using UnityEngine;
using System.Collections;

public class GrabController : MonoBehaviour {

	Transform player, grabPoint, grabStart, grabForward;
	SphereCollider grabCollider;

	bool grabbed = false;
	GameObject grabbedObject;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		grabPoint = transform.FindChild ("GrabPoint");
		grabForward = transform.FindChild ("GrabForward");
		grabStart = grabPoint;
		grabCollider = grabPoint.GetComponent<SphereCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !grabbed) {
			grabbedObject = GetGrabbableObject ();
			if (grabbedObject != null) {
				grabbed = true;
				Debug.Log ("Grabbed: " + grabbedObject.name);
				HingeJoint joint = grabbedObject.AddComponent<HingeJoint> ();
				joint.connectedBody = grabPoint.GetComponent<Rigidbody>();
				joint.anchor = grabPoint.position;
				grabbedObject.layer |= LayerMask.NameToLayer ("Grabbed");
				Debug.Log ("Layer number: "+LayerMask.NameToLayer("Grabbed"));
			}
		}

		if (Input.GetKeyUp (KeyCode.Mouse0) && grabbed) {
			grabbed = false;
			Destroy (grabbedObject.GetComponent<HingeJoint> ());
			grabbedObject.layer = 0;

			grabbedObject = null;
		}

		if (grabbed) {

		}
	}


	GameObject GetGrabbableObject(){
		GameObject obj = null;
		Debug.DrawRay (grabStart.parent.position, GrabDirection(), Color.blue, 2f);

		RaycastHit[] hits = Physics.SphereCastAll (grabStart.parent.position, 1.5f, GrabDirection());
		foreach (RaycastHit hit in hits) {
			if (hit.transform.tag == "Grabbable") {
				obj = hit.transform.gameObject;
				grabPoint.position = hit.point;


			}
		}
		return obj;
	}

	Vector3 GrabDirection() {
		return grabStart.parent.position - grabForward.position;
	}
}
