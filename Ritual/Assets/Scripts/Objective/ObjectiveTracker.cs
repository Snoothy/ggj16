using UnityEngine;
using System.Collections;

public class ObjectiveTracker : MonoBehaviour {

	public ObjectiveHandler.Objectives type;
	public Transform attachPoint;
	public Vector3 rotation;
	ObjectiveHandler handler;
	GrabController grab;

	bool completed = false;


	// Use this for initialization
	void Start () {
		handler = GameObject.FindGameObjectWithTag ("UI").GetComponent<ObjectiveHandler> ();
		grab = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<GrabController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Product" && !completed) {
			handler.CompleteObjective (type, other.transform.name);
			HandleProduct (other.transform);
			grab.StopGrab ();
			Destroy (other.gameObject.GetComponent<Rigidbody>());
			other.transform.tag = "Untagged";
			completed = true;
		}
	}

	protected virtual void HandleProduct(Transform other){
		RenestFirstSprite (other, attachPoint);
	}

	Transform RenestFirstSprite(Transform product, Transform parent){
		//Transform sprite = product.FindChild ("MeshParent").GetChild (0);
		Transform sprite = product;
		sprite.parent = parent;
		sprite.localPosition = Vector3.zero;
		sprite.localRotation = Quaternion.Euler(rotation);
		return attachPoint;
	}
}
