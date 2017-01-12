using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CastLine : MonoBehaviour
{
    // Transform belonging to the GameObject where the linecast will end
    public Transform otherConnector;

    // Layer(s) that the linecast will use for collision
    public LayerMask layerMask;
    public Text linecatUIText;

    // Transform belonging to the GameObject where the linecast will begin
    private Transform connector;
    private int collisions;

    void Start()
    {
        // In this example, a child GaemObject named Connector is used as the linecast starting point
        connector = transform.Find("Connector").transform;
    }

    void Update()
    {
        // Make the linecast visible in the Scene view when debugging the game
        Debug.DrawLine(connector.position, otherConnector.position, Color.blue);

        // RayCastHit will hold information about the collision (not used in this example)
        RaycastHit hit;

        // Create a linecast between start and end positions. Check if anything on the set layer(s) collides with it.
        if(Physics.Linecast(connector.position, otherConnector.position, out hit, layerMask))
        {
            // If so...
            collisions++;
            //Debug.Log("Linecast name: " + hit.collider.gameObject.name);
            //Debug.Log("Linecast hit point: " + hit.point);

            linecatUIText.text = "Linecasts are similar to raycasts, but are used when both start and end points are known. \n\nCollisions: " + collisions;
        }
    }
}
