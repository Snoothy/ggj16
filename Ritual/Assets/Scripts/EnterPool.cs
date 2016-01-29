using UnityEngine;

public class EnterPool : MonoBehaviour {

    private CombinationTable table;

	// Use this for initialization
	void Start () {
        table = this.GetComponentInParent<CombinationTable>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        Item item = collision.gameObject.GetComponent<Item>();
        if (item != null)
        {
            if (item is Ingredient)
            {
                table.OnEnterCollision(item as Ingredient);
            } else if (item is Product)
            {
                table.OnEnterCollision(item as Product);
            }
        }
    }
}
