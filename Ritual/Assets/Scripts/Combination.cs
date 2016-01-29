using UnityEngine;

[System.Serializable]
public class Combination {
    public IngredientType[] ingredients;
    public GameObject product;

    // NOTE: This assumes that the ingredients are not altered during runtime.
    private IngredientType bitmaskCache = 0;
    private bool isCacheSet = false;

    public IngredientType bitmask
    {
        get
        {
            IngredientType mask = 0;
            if (isCacheSet)
            {
                mask = bitmaskCache;
            }
            else {
                if (ingredients != null)
                {
                    foreach (IngredientType type in ingredients)
                    {
                        mask |= type;
                    }
                    bitmaskCache = mask;
                    isCacheSet = true;
                }
                else
                    throw new System.Exception("The list of ingredients cannot be null");
            }
            return mask;
        }
    }
}
