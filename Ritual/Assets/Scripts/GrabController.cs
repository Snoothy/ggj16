using UnityEngine;
using System.Collections;

public class GrabController : MonoBehaviour {

	Transform grabPoint, grabStart, grabForward;

	bool grabbed = false;
	GameObject grabbedObject;
	float grabDistance = 2f;

	public AnimationCurve c;// = new AnimationCurve ();
	public AnimationCurve speed;

	// Use this for initialization
	void Start () {
		grabPoint = transform.FindChild ("GrabPoint");//.FindChild("GrabOffset").FindChild("GrabChild").FindChild ("GrabPoint");
		grabForward = transform.FindChild ("GrabForward");
		grabStart = transform.FindChild ("GrabPoint");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Mouse0) && !grabbed) {
			grabbedObject = GetGrabbableObject ();
			if (grabbedObject != null) {
				grabbed = true;
				Debug.Log ("Grabbed: " + grabbedObject.name);

				SpringJoint joint = grabbedObject.GetComponent<SpringJoint>();
				if (joint == null)
					joint = grabbedObject.AddComponent<SpringJoint> ();
				joint.connectedBody = grabPoint.GetComponent<Rigidbody>();
				joint.anchor = Vector3.zero;// grabPoint.position;
				joint.spring = 10000f;
				//joint.breakForce = 100000f;
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
			SpringJoint j = grabbedObject.GetComponent<SpringJoint> ();
			float grabPercent = Mathf.Clamp (((grabDistance - Vector3.Distance (grabbedObject.transform.position, grabPoint.position)) / grabDistance), 0f, 1f);
			j.damper = c.Evaluate (grabPercent)*10000f;
			j.spring = speed.Evaluate(grabPercent) * 10000f;
			float maxSpeed = 2.5f;
			if (Vector3.Magnitude (grabbedObject.GetComponent<Rigidbody> ().velocity) > maxSpeed) {
				grabbedObject.GetComponent<Rigidbody> ().velocity = Vector3.Normalize (grabbedObject.GetComponent<Rigidbody> ().velocity) * maxSpeed;
			}
		}
	}

    public GameObject getGrabbedObject ()
    {
        return grabbedObject;
    }

	public void StopGrab(){
		Destroy (grabbedObject.GetComponent<SpringJoint> ());
		grabbedObject.layer = 0;
		grabbedObject.GetComponent<Rigidbody> ().useGravity = true;
		grabbedObject = null;
		grabbed = false;
	}

	GameObject GetGrabbableObject(){
		GameObject obj = null;
		Debug.DrawRay (grabStart.parent.position, Vector3.Normalize( GrabDirection()) * grabDistance, Color.blue, 2f);

		RaycastHit[] hits = Physics.SphereCastAll (grabStart.parent.position, .25f, Vector3.Normalize(GrabDirection()),grabDistance);

		//RaycastHit hit = new RaycastHit();
		//Physics.Raycast (grabStart.parent.position, Vector3.Normalize (GrabDirection ()), out hit, grabDistance);
		foreach (RaycastHit hit in hits) {
			if (hit.transform.tag == "Grabbable") {
				obj = hit.transform.gameObject;
				//grabPoint.position = hit.transform.position + hit.transform.GetComponent<Rigidbody>().centerOfMass;
				//grabPoint.localPosition = new Vector3 (0f, 0f, grabPoint.localPosition.z);
			}
		}

		//if (hit.transform.tag == "Grabbable") {
		//	obj = hit.transform.gameObject;
		//}
		return obj;
	}

	Vector3 GrabDirection() {
		return  grabForward.position - grabStart.parent.position;
	}
}
