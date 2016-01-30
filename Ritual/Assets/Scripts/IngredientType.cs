using System;

[System.Flags]
public enum IngredientType {
    Book = 1 << 1,
    Cross = 1 << 2,
    CandleLight = 1 << 3,
	Plant = 1 << 4,
	Blender = 1 << 5,
	Can = 1 << 6,
	Circle = 1 << 7,
	Arm = 1 << 8,
	Egg = 1 << 9,
	Flour = 1 << 10,
	Water = 1 << 11,
	Scissors = 1 << 12,
	Paper = 1 << 13,
	Dish = 1 << 14
}

public static class IngredientTypeTools {

    public static IngredientType getRandomIngredientType (Random random = null)
    {
        Array values = Enum.GetValues(typeof(IngredientType));
        random = random != null ? random : new Random();
        return (IngredientType)values.GetValue(random.Next(values.Length));
    }
}
