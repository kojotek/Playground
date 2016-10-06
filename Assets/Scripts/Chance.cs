using UnityEngine;

public static class Chance
{
    public static bool GetTrueWithChance(float chance)
    {
        //Debug.Log(chance);
        return Random.value <= chance; 
    }
}
