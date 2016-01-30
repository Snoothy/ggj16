using UnityEngine;
using System.Collections.Generic;

public class MaterialMeshPair
{
    public Material material;
    public MeshRenderer item;

    public MaterialMeshPair(Material material, MeshRenderer item)
    {
        this.material = material;
        this.item = item;
    }
}

public class MaterialSpritePair
{
    public Material material;
    public SpriteRenderer item;

    public MaterialSpritePair(Material material, SpriteRenderer item)
    {
        this.material = material;
        this.item = item;
    }
}

[RequireComponent(typeof(GrabController))]
public class HighlightGrabbableObject : MonoBehaviour {

    public Material outlineMaterialMesh;
    public Material outlineMaterialSprite;

    private Item highlightedObject;
    private List<MaterialMeshPair> meshMaterialPairs;
    private List<MaterialSpritePair> spriteMaterialPairs;

    private GrabController grabController;

    void Start ()
    {
        grabController = this.GetComponent<GrabController>();
    }

    void LateUpdate ()
    {
        GameObject grabbedObject = grabController.getGrabbedObject();
        GameObject grabbableObject = grabController.GetGrabbableObject();
        GameObject objectToHighlight = null;

        if (grabbedObject != null)
        {
            objectToHighlight = grabbedObject;
        } else if (grabbableObject != null)
        {
            objectToHighlight = grabbableObject;
        }

        if (highlightedObject != null && (objectToHighlight == null || objectToHighlight != highlightedObject.gameObject))
        {
            foreach (MaterialMeshPair pair in meshMaterialPairs)
            {
                pair.item.material = pair.material;
            }
            foreach (MaterialSpritePair pair in spriteMaterialPairs)
            {
                pair.item.material = pair.material;
            }
            highlightedObject = null;
            meshMaterialPairs = null;
            spriteMaterialPairs = null;
        }
        if (objectToHighlight != null) {
            Item item = objectToHighlight.GetComponent<Item>();
			if (item != null) {
                highlightedObject = item;
                meshMaterialPairs = new List<MaterialMeshPair>();
                spriteMaterialPairs = new List<MaterialSpritePair>();
                foreach (MeshRenderer mesh in highlightedObject.GetComponentsInChildren<MeshRenderer>())
                {
                    meshMaterialPairs.Add(new MaterialMeshPair(mesh.material, mesh));
                    mesh.material = Instantiate(outlineMaterialMesh);
                }
                foreach (SpriteRenderer spr in highlightedObject.GetComponentsInChildren<SpriteRenderer>())
                {
                    spriteMaterialPairs.Add(new MaterialSpritePair(spr.material, spr));
                    spr.material = Instantiate(outlineMaterialSprite);
                }
            }
        }
    }
}
