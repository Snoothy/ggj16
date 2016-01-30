using UnityEngine;
using System.Collections;

public class ObjectiveTracker : MonoBehaviour {

	public ObjectiveHandler.Objectives type;
	public Transform attachPoint;
	public GameObject outline;
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
			handler.CompleteObjective (type, other.transform.GetComponent<Product>().productName.ToLower());
			HandleProduct (other.transform);
			grab.StopGrab ();
			Destroy (other.gameObject.GetComponent<Rigidbody>());
			other.transform.tag = "Untagged";
			outline.SetActive(false);
			completed = true;
		} else if (other.gameObject.GetComponent<Ingredient>())
        {
            GrabController grabController = GameObject.FindObjectOfType<GrabController>();
            if (grabController.getGrabbedObject() == other.gameObject)
            {
                grabController.StopGrab();
            }
            other.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(5, 5, 5), other.transform.position, ForceMode.Impulse);
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
