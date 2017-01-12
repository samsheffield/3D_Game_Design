using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountCollisions : MonoBehaviour
{
    public Text collisionUIText;

    private int collisions;

    public void OnTriggerEnter(Collider other)
    {
        collisions++;
        collisionUIText.text = "Raycasting/linecasting can be costly. Using layers and/or trigger colliders can sometimes reduce overhead.\n\nCollisions: " + collisions;
    }

}
