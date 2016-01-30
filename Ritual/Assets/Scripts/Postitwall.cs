using UnityEngine;

public class Postitwall : MonoBehaviour {

    public GameObject genericPostIt;
    public Material genericPostItMaterial;

    public Transform spot1;
    private GameObject object1;
    public Transform spot2;
    private GameObject object2;
    public Transform spot3;
    private GameObject object3;

    private bool tryToGetTexture(Item ingredient, out Sprite sprite)
    {
        if (ingredient.GetComponentInChildren<SpriteRenderer>() != null)
        {
            sprite = ingredient.GetComponentInChildren<SpriteRenderer>().sprite;
            return true;
        }
        sprite = null;
        return false;
    }

    public void addIngredient(Item ingredient)
    {
        Sprite sprite;
        if (tryToGetTexture(ingredient, out sprite))
        {
            GameObject obj;
            Transform spot;
            if (object1 == null)
            {
                object1 = obj = Instantiate(genericPostIt, spot1.position, spot1.rotation) as GameObject;
                spot = spot1;
            }
            else if (object2 == null)
            {
                object2 = obj = Instantiate(genericPostIt, spot2.position, spot2.rotation) as GameObject;
                spot = spot2;
            }
            else if (object3 == null)
            {
                object3 = obj = Instantiate(genericPostIt, spot3.position, spot3.rotation) as GameObject;
                spot = spot3;
            }
            else
                throw new System.Exception("Too many ingredients received");

            obj.transform.parent = spot;
            obj.transform.localScale = Vector3.one;
            obj.GetComponentInChildren<MeshRenderer>().material = Instantiate(genericPostItMaterial);
            obj.GetComponentInChildren<MeshRenderer>().material.mainTexture = sprite.texture;
        }
        else
            Debug.LogWarning("Sprite not found");
    }

    public void Clear ()
    {
        Destroy(object1);
        Destroy(object2);
        Destroy(object3);
    }
}
