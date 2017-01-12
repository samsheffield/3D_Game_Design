// Basic third person 3D movement using forces
using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
    public float speed;     // How fast to move the player
    private Rigidbody rb;   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Physics-based movement should take place in FixedUpdate()
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);      // Add a force in the direction of movement multiplied by speed
    }
}
