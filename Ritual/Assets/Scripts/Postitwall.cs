using UnityEngine;
using System.Collections.Generic;

public class Postitwall : MonoBehaviour {

    public List<IngredientTypeToPostIt> postits = new List<IngredientTypeToPostIt>();

    public Transform spot1;
    private GameObject object1;
    public Transform spot2;
    private GameObject object2;
    public Transform spot3;
    private GameObject object3;

    private bool tryToGetTexture (IngredientType ingredient, out GameObject postitprefab)
    {
        foreach (IngredientTypeToPostIt postit in postits)
        {
            if (postit.type == ingredient)
            {
                postitprefab = postit.postit;
                return true;
            }
        }
        postitprefab = null;
        return false;
    }

    public void addIngredient (IngredientType ingredient)
    {
        GameObject postit;
        if (tryToGetTexture(ingredient, out postit))
        {
            if (object1 == null)
            {
                object1 = Instantiate(postit, spot1.position, spot1.rotation) as GameObject;
                object1.transform.parent = spot1;
                object1.transform.localScale = Vector3.one;
            }
            else if (object2 == null)
            {
                object2 = Instantiate(postit, spot2.position, spot2.rotation) as GameObject;
                object2.transform.parent = spot2;
                object2.transform.localScale = Vector3.one;
            }
            else if (object3 == null)
            {
                object3 = Instantiate(postit, spot3.position, spot3.rotation) as GameObject;
                object3.transform.parent = spot3;
                object3.transform.localScale = Vector3.one;
            }
            else
                throw new System.Exception("Too many ingredients received");
        } else
            Debug.LogWarning("Postit not found");
    }

    public void Clear ()
    {
        Destroy(object1);
        Destroy(object2);
        Destroy(object3);
    }
}
