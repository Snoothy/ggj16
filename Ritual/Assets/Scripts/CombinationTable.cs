using UnityEngine;
using System.Collections.Generic;
using LayerManagement.Action.Finite;
using LayerManagement.Action.Infinite;

[System.Serializable]
public class SuckDownAnimationConfiguration
{
    public float fromHeight = 0.45f;
    public float toOut = 0.25f;
    public float rotationSpeed = 45;
    public float downTime = 3;
}

public class CombinationTable : MonoBehaviour {

    public Transform enterPoolParent;
    public Transform exitPoolParent;
    public Postitwall postitwall;
    public AudioClip wrongAnswerClip;
    public AudioClip onCorrectCombination;
    public AudioClip[] onIncorrectCombination;

    public List<Combination> combinations = new List<Combination>();
    public List<Product> badproducts = new List<Product>();
    private List<Product> badproductsStack = null;

    private IngredientType currentPool = 0;
    private int noIngredients = 0;

    public void addIngredientToCombination(Item item)
    {
        IngredientType type = item is Ingredient ? (item as Ingredient).ingredientType : IngredientTypeTools.getRandomIngredientType();

        if (!addIngredientToCombination(type))
        {
            item.gameObject.AddComponent<Rigidbody>();
            item.GetComponent<Collider>().enabled = true;
            placeProduct(item.gameObject);
        } else
        {
            Destroy(item.gameObject);
        }
    }

    private GameObject getBadProduct ()
    {
        if (badproductsStack == null)
        {
            badproductsStack = new List<Product>();
        }
        if (badproductsStack.Count == 0)
        {
            badproductsStack.AddRange(badproducts);
        }

        GameObject badProduct;

        int i = Random.Range(0, badproductsStack.Count);

        badProduct = badproductsStack[i].gameObject;
        badproductsStack.RemoveAt(i);

        return badProduct;
    }

    public bool addIngredientToCombination(IngredientType item)
    {
        // BUG: this check fails if two same typed items are added to the pool before the first item has completed its downscale animation
        if (noIngredients == 0 || (currentPool & item) != item)
        {
            currentPool |= item;
            noIngredients++;
            GameObject product;
            if (isIngredientsAProduct(currentPool, out product))
            {
                currentPool = 0;
                noIngredients = 0;
                instantiateProduct(product);
                postitwall.Clear();
                enterPoolParent.GetComponentInChildren<EnterPool>().GetComponent<MeshCollider>().enabled = true;
                this.GetComponent<AudioSource>().PlayOneShot(onCorrectCombination);
            }
            else if (noIngredients == 3)
            {
                currentPool = 0;
                noIngredients = 0;
                postitwall.Clear();
                Debug.LogWarning("No combination was found");
                instantiateProduct(this.getBadProduct());
                enterPoolParent.GetComponentInChildren<EnterPool>().GetComponent<MeshCollider>().enabled = true;
                this.GetComponent<AudioSource>().PlayOneShot(onIncorrectCombination[Random.Range(0, onIncorrectCombination.Length)]);
            }
            return true;
        }
        return false;
    }

    private bool isIngredientsAProduct(IngredientType pool, out GameObject product)
    {
        int c = combinations.Count;
        for (int i = 0; i < c; i++)
        {
            Combination combination = combinations[i];
            if (combination.bitmask == pool)
            {
                product = combination.product;
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
        AudioSource audioSource = item.gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = item.gameObject.AddComponent<AudioSource>();
        }

        audioSource.PlayOneShot(wrongAnswerClip, 0.3f);
    }

    public void animateEnter (Item item)
    {
        GrabController grabController = GameObject.FindObjectOfType<GrabController>();
        if (grabController.getGrabbedObject() == item.gameObject)
        {
            grabController.StopGrab();
        }

        Destroy(item.GetComponent<Rigidbody>());
        item.GetComponent<Collider>().enabled = false;

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
        scale.delegates += a =>
        {
            addIngredientToCombination(item);
            Destroy(spinParent);
            Destroy(item.gameObject.GetComponent<MoveToVectorByTime>());
            Destroy(a);
        };
        scale.runActionWith(new ScaleToByTimeInfo(spinConfigurations.downTime, Vector3.zero, 0));
        addingItemToPool(item);
    }

    private void addingItemToPool(Item item)
    {
        IngredientType type = item is Ingredient ? (item as Ingredient).ingredientType : IngredientTypeTools.getRandomIngredientType();

        if (noIngredients == 0 || (currentPool & type) != type)
        {
            postitwall.addIngredient(item);
        }

        IngredientType pool = currentPool | type;
        GameObject product;
        if (isIngredientsAProduct(pool, out product))
        {
            enterPoolParent.GetComponentInChildren<EnterPool>().GetComponent<MeshCollider>().enabled = false;
        }
    }

    private void instantiateProduct(GameObject product)
    {
        Debug.LogWarning("new product" + product.name);
        placeProduct(Instantiate(product));
    }

    private void placeProduct (GameObject product)
    {
        Destroy(product.GetComponent<Rigidbody>());
        product.GetComponent<Collider>().enabled = false;

        GameObject spinParent = new GameObject("SpinParent");
        spinParent.transform.parent = exitPoolParent;
        spinParent.transform.localScale = Vector3.one;
        spinParent.transform.localRotation = Quaternion.identity;
        spinParent.transform.localPosition = Vector3.zero;

        product.transform.parent = spinParent.transform;
        product.transform.rotation = Quaternion.identity;
        product.transform.localScale = Vector3.zero;
        product.transform.localPosition = Vector3.zero;

        spinParent.AddComponent<RotateBy>().runActionWith(new RotateByInfo(spinConfigurations.rotationSpeed, new Vector3(0, 1, 0)));

        product.gameObject.AddComponent<MoveToVectorByTime>().runActionWith(new MoveToVectorByTimeInfo(spinConfigurations.downTime, new Vector3(spinConfigurations.toOut,0,0), true, 0));
        spinParent.AddComponent<MoveToVectorByTime>().runActionWith(new MoveToVectorByTimeInfo(spinConfigurations.downTime, spinParent.transform.position.LayerManagementSetY(spinParent.transform.position.y + this.spinConfigurations.fromHeight*0.5f), false, 0));

        ScaleToByTime scale = product.gameObject.AddComponent<ScaleToByTime>();
        scale.delegates += a =>
        {
            product.gameObject.AddComponent<Rigidbody>();
            product.GetComponent<Collider>().enabled = true;
            product.transform.parent = null;
            Destroy(spinParent);
            Destroy(product.gameObject.GetComponent<MoveToVectorByTime>());
            Destroy(a);
        };
        scale.runActionWith(new ScaleToByTimeInfo(spinConfigurations.downTime, Vector3.one, 0));
    }
}
