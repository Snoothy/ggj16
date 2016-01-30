using UnityEngine;
using System.Collections.Generic;

public class MaterialGameObjectPair
{
    public Material material;
    public MeshRenderer item;

    public MaterialGameObjectPair(Material material, MeshRenderer item)
    {
        this.material = material;
        this.item = item;
    }
}

[RequireComponent(typeof(GrabController))]
public class HighlightGrabbableObject : MonoBehaviour {

    public Material outlineMaterial;

    private Item highlightedObject;
    private List<MaterialGameObjectPair> highlightedObjectOriginalMaterial;

    private GrabController grabController;

    void Start ()
    {
        grabController = this.GetComponent<GrabController>();
    }

    void LateUpdate ()
    {
        if (highlightedObject != null)
        {
            foreach (MaterialGameObjectPair pair in highlightedObjectOriginalMaterial)
            {
                pair.item.material = pair.material;
            }
            highlightedObject = null;
            highlightedObjectOriginalMaterial = null;
        }
        if (grabController.getGrabbedObject() == null)
        {
            GameObject grabbableObject = grabController.GetGrabbableObject();
            if (grabbableObject != null)
            {
                Item item = grabbableObject.GetComponent<Item>();
                highlightedObject = item;
				if (highlightedObject != null) {
                    highlightedObjectOriginalMaterial = new List<MaterialGameObjectPair>();
                    foreach (MeshRenderer mesh in highlightedObject.GetComponentsInChildren<MeshRenderer>())
                    {
                        highlightedObjectOriginalMaterial.Add(new MaterialGameObjectPair(mesh.material, mesh));
                        mesh.material = Instantiate(outlineMaterial);
                    }
				}
            }
        }
    }
}
