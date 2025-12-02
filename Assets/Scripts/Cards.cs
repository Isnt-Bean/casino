using UnityEngine;

public class Cards : MonoBehaviour
{
    public int value = 0;


    public int GetValueCard()
    {
        return value;
    }

    public void SetValue(int newValue)
    {
        value = newValue;
    }

    public string GetSpriteName()
    {
        return GetComponent<SpriteRenderer>().sprite.name;
    }
    
    
}
