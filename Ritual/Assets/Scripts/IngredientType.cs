using System;

[System.Flags]
public enum IngredientType {
    Book = 1,
    Cross = 2,
    CandleLight = 4
}

public static class IngredientTypeTools {

    public static IngredientType getRandomIngredientType (Random random = null)
    {
        Array values = Enum.GetValues(typeof(IngredientType));
        random = random != null ? random : new Random();
        return (IngredientType)values.GetValue(random.Next(values.Length));
    }
}
