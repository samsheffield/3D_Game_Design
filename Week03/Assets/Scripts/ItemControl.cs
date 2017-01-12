using UnityEngine;
using System.Collections;

public class ItemControl : MonoBehaviour {

    // Prefab used as an explosion
    public GameObject explosion;

    // Use an enum to set the numeric value of this item from the Inspector
    public enum Value
    {
        basic = 1,
        good = 2,
        superior = 5
    }
    public Value value = Value.basic;

    // Call this method to get the value of this item. This returns an int
	public int GetValue()
    {
        return (int)value; // Cast the enum as an int before passing it along
    }

    // Other scripts can call this method to create an explosion
    public void Explode()
    {
        // Instantiate an explosion where this GameOject is located
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
