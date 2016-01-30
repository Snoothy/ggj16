using UnityEngine;
using System.Collections.Generic;
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
    public Transform enterPoolParent;
    public Postitwall postitwall;

    public List<Combination> combinations = new List<Combination>();
    private IngredientType currentPool = 0;

    public void addIngredientToCombination(Item item)
    {
        IngredientType type = item is Ingredient ? (item as Ingredient).ingredientType : IngredientTypeTools.getRandomIngredientType();
        Destroy(item.gameObject);
        addIngredientToCombination(type);
    }

    public void addIngredientToCombination(IngredientType item)
    {
        currentPool |= item;
        postitwall.addIngredient(item);
        GameObject product;
        if (isIngredientsAProduct(currentPool, out product))
        {
            currentPool = 0;
            instantiateProduct(product);
            postitwall.Clear();
        }
    }

    private bool isIngredientsAProduct(IngredientType pool, out GameObject product)
    {
        int c = combinations.Count;
        Debug.LogWarning("c=" + c);
            Debug.LogWarning("pool=" + pool.ToString());
        for (int i = 0; i < c; i++)
        {
            Combination combination = combinations[i];
            Debug.LogWarning("combination=" + combination.bitmask.ToString());
            if (combination.bitmask == pool)
            {
                product = combination.product;
                Debug.Log(currentPool.ToString() + "==" + combination.bitmask.ToString());
                return true;
            }
        }
        product = null;
        return false;
    }

    public SuckDownAnimationConfiguration spinConfigurations;

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
        scale.delegates += a => addIngredientToCombination(item);
        scale.runActionWith(new ScaleToByTimeInfo(spinConfigurations.downTime, Vector3.zero, 0));
    }

    public void instantiateProduct(GameObject product)
    {
        Debug.LogWarning("new product" + product.name);
        Instantiate(product, spawnProductAt.position, Quaternion.identity);
    }
}
