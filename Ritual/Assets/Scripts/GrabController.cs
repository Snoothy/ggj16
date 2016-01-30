using UnityEngine;
using System.Collections;

public class GrabController : MonoBehaviour {

	Transform grabPoint, grabStart, grabForward;

	bool grabbed = false;
	GameObject grabbedObject;

	// Use this for initialization
	void Start () {
		grabPoint = transform.FindChild ("GrabPoint");
		grabForward = transform.FindChild ("GrabForward");
		grabStart = grabPoint;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !grabbed) {
			grabbedObject = GetGrabbableObject ();
			if (grabbedObject != null) {
				grabbed = true;
				Debug.Log ("Grabbed: " + grabbedObject.name);

				HingeJoint joint = grabbedObject.GetComponent<HingeJoint>();
				if (joint == null)
					joint = grabbedObject.AddComponent<HingeJoint> ();
				joint.connectedBody = grabPoint.GetComponent<Rigidbody>();
				joint.anchor = Vector3.zero;// grabPoint.position;
				joint.breakForce = 100000f;
				//joint.projectionMode = JointProjectionMode.PositionAndRotation;
				//SoftJointLimitSpring lim = joint.linearLimitSpring;
				//lim.spring = 10000f;
				//lim.damper = 5f;
				//joint.linearLimitSpring = lim;
				//joint.xMotion = ConfigurableJointMotion.Limited;
				//joint.yMotion = ConfigurableJointMotion.Limited;
				//joint.zMotion = ConfigurableJointMotion.Limited;


				grabbedObject.GetComponent<Rigidbody> ().useGravity = false;
				//joint.breakForce = 10000f;

				grabbedObject.layer |= LayerMask.NameToLayer ("Grabbed");
			}
		}

		if (Input.GetKeyUp (KeyCode.Mouse0) && grabbed) {
			StopGrab ();
		}

		if (grabbed) {

		}
	}

    public GameObject getGrabbedObject ()
    {
        return grabbedObject;
    }

	public void StopGrab(){
		Destroy (grabbedObject.GetComponent<HingeJoint> ());
		grabbedObject.layer = 0;
		grabbedObject.GetComponent<Rigidbody> ().useGravity = true;
		grabbedObject = null;
		grabbed = false;
	}

	GameObject GetGrabbableObject(){
		GameObject obj = null;
		float grabDistance = 2f;
		Debug.DrawRay (grabStart.parent.position, Vector3.Normalize( GrabDirection()) * grabDistance, Color.blue, 2f);

		RaycastHit[] hits = Physics.SphereCastAll (grabStart.parent.position, .25f, Vector3.Normalize(GrabDirection()),grabDistance);

		foreach (RaycastHit hit in hits) {
			if (hit.transform.tag == "Grabbable") {
				obj = hit.transform.gameObject;
				grabPoint.position = hit.transform.position + hit.transform.GetComponent<Rigidbody>().centerOfMass;


			}
		}
		return obj;
	}

	Vector3 GrabDirection() {
		return  grabForward.position - grabStart.parent.position;
	}
}
