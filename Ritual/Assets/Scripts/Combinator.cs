using UnityEngine;
using System.Collections.Generic;

public class Combinator : MonoBehaviour {

    public List<Combination> combinations = new List<Combination>();
    private IngredientType currentPool = 0;

    public CombinationTable table;
    public Postitwall postitwall;

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
            table.instantiateProduct(product);
            postitwall.Clear();
        }
    }

    private bool isIngredientsAProduct (IngredientType pool, out GameObject product)
    {
        int c = combinations.Count;
        for (int i = 0; i < c; i++)
        {
            Combination combination = combinations[i];
            if (combination.bitmask == pool)
            {
                product = combination.product;
                Debug.Log(currentPool.ToString()+"=="+combination.bitmask.ToString());
                return true;
            }
        }
        product = null;
        return false;
    }
}
