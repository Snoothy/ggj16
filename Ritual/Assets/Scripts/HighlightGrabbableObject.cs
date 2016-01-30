using UnityEngine;

[RequireComponent(typeof(GrabController))]
public class HighlightGrabbableObject : MonoBehaviour {

    public Material outlineMaterial;

    private Item highlightedObject;
    private Material highlightedObjectOriginalMaterial;

    private GrabController grabController;

    void Start ()
    {
        grabController = this.GetComponent<GrabController>();
    }

    void LateUpdate ()
    {
        if (highlightedObject != null)
        {
            highlightedObject.meshRendererForOutline.material = highlightedObjectOriginalMaterial;
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
                highlightedObjectOriginalMaterial = item.meshRendererForOutline.material;

                item.meshRendererForOutline.material = Instantiate(outlineMaterial);
            }
        }
    }
}
