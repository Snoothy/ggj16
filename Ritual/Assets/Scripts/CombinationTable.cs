using UnityEngine;

public class CombinationTable : MonoBehaviour {

    public Transform spawnProductAt;

    public Combinator combinator;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnEnterCollision (Ingredient item)
    {
        combinator.addIngredientToCombination(item);
    }

    public void OnEnterCollision(Product item)
    {
        combinator.addIngredientToCombination(item);
    }

    public void instantiateProduct(GameObject product)
    {
        Instantiate(product, spawnProductAt.position, Quaternion.identity);
    }
}
