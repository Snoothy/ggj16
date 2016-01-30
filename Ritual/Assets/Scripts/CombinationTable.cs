using UnityEngine;
using LayerManagement.Action.Finite;
using LayerManagement.Action.Infinite;

[System.Serializable]
public class SuckDownAnimationConfiguration
{
    public float fromHeight = 0.45f;
    public float rotationSpeed = 45;
    public float downTime = 3;
}

public class CombinationTable : MonoBehaviour {

    public Transform spawnProductAt;
    public Combinator combinator;
    public Transform enterPoolParent;

    public SuckDownAnimationConfiguration spinConfigurations;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnEnterCollision (Ingredient item)
    {
        animateEnter(item);
    }

    public void OnEnterCollision(Product item)
    {
        GrabController grabController = GameObject.FindObjectOfType<GrabController>();
        if (grabController.getGrabbedObject() == item.gameObject)
        {
            grabController.StopGrab();
        }
        item.gameObject.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(5, 5, 5), enterPoolParent.transform.position, ForceMode.Impulse);
    }

    public void animateEnter (Item item)
    {
        GrabController grabController = GameObject.FindObjectOfType<GrabController>();
        if (grabController.getGrabbedObject() == item.gameObject)
        {
            grabController.StopGrab();
        }

        foreach (Rigidbody rigidbody in item.GetComponentsInChildren<Rigidbody>())
        {
            Destroy(rigidbody);
        }

        foreach (Collider collider in item.GetComponentsInChildren<Collider>())
        {
            Destroy(collider);
        }

        GameObject spinParent = new GameObject("SpinParent");
        spinParent.transform.parent = enterPoolParent;
        spinParent.transform.localScale = Vector3.one;
        spinParent.transform.localRotation = Quaternion.identity;
        spinParent.transform.localPosition = Vector3.zero;
        spinParent.transform.position = spinParent.transform.position.LayerManagementSetY(spinParent.transform.position.y + this.spinConfigurations.fromHeight);

        item.transform.parent = spinParent.transform;
        spinParent.AddComponent<RotateBy>().runActionWith(new RotateByInfo(spinConfigurations.rotationSpeed, new Vector3(0, -1, 0)));

        item.gameObject.AddComponent<MoveToVectorByTime>().runActionWith(new MoveToVectorByTimeInfo(spinConfigurations.downTime, Vector3.zero, true, 0));
        spinParent.AddComponent<MoveToVectorByTime>().runActionWith(new MoveToVectorByTimeInfo(spinConfigurations.downTime, Vector3.zero, true, 0));

        ScaleToByTime scale = item.gameObject.AddComponent<ScaleToByTime>();
        scale.delegates += a => combinator.addIngredientToCombination(item);
        scale.runActionWith(new ScaleToByTimeInfo(spinConfigurations.downTime, Vector3.zero, 0));
    }

    public void instantiateProduct(GameObject product)
    {
        Instantiate(product, spawnProductAt.position, Quaternion.identity);
    }
}
