using UnityEngine;
using System.Collections;

public class ObjectiveTracker : MonoBehaviour {

	public ObjectiveHandler.Objectives type;
	ObjectiveHandler handler;
	GrabController grab;


	// Use this for initialization
	void Start () {
		handler = GameObject.FindGameObjectWithTag ("UI").GetComponent<ObjectiveHandler> ();
		grab = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<GrabController> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other){
		if (other.transform.tag == "Product") {
			handler.CompleteObjective (type, other.transform.name);
			HandleProduct (other.transform);
			grab.StopGrab ();
			Destroy (other.gameObject);
		}
	}

	public virtual void HandleProduct(Transform other){

	}
}
