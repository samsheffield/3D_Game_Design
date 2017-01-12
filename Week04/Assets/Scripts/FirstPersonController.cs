// Basic FPS-style movement controls. Updated with jump controls.
// Portions of this script are based on code from Unity in Action: https://www.manning.com/books/unity-in-action
// Physics portion of this script is based on Unity documentation: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnControllerColliderHit.html

using UnityEngine;
using System.Collections;

// This script needs a CharacterController coomponent
[RequireComponent(typeof(CharacterController))]

public class FirstPersonController : MonoBehaviour
{
    public float speed = 6f;            // Movement speed
    public float jumpSpeed = 4f;        // Speed to move upward
    public float jumpScaler = 5f;       // Higher number decreases the time hanging in the air
    public float fallRate = -1.5f;      // Rate to gradually move downward by
    public float gravity = -9.8f;       // Simulated gravity (CharacterController will not work with Rigidbody)

    // OPTIONAL
    public bool usePhysicsCollision;    // Should ohysics collisions with Rigidbodies be simulated? 
    public float pushPower = 1f;        // If using physics collision, this is the force applied when hitting a Rigidbody.

    private CharacterController cc;     // Attached CharacterController component.
    private float ySpeed;               // Current speed of movement on y axis (heading up or heading down?)

    void Start()
    {
        cc = GetComponent<CharacterController>();   // Get the CharacterController component.
    }

    void Update()
    {

        // Movement is based on the current axis position times speed.
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        // Ground movement
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);  // Ground-based movement. Y will be handled below
        movement = Vector3.ClampMagnitude(movement, speed); // Limit movement speed

        // Jumping
        // Check if the CharacterController's is on top of something (isGrounded = true). CharacterController reference: https://docs.unity3d.com/ScriptReference/CharacterController.html
        if (cc.isGrounded)
        {
            // If the player is jumping the y speed is increased, otherwise it will be decreased at a slower rate
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpSpeed;
            }
            else
            {
                ySpeed = fallRate;
            }
        }
        else
        {
            // If the CharacterController is not grounded...
            // Gradually add to the y speed, scale it so that it happens faster, and make it framerate independednt. 
            ySpeed += gravity * jumpScaler * Time.deltaTime;

            // Limit the downward speed to gravity
            if (ySpeed < gravity)
            {
                ySpeed = gravity;
            }
        }

        movement.y = ySpeed;                               // Apply simulated gravity to Y.
        movement *= Time.deltaTime;                         // Make movement consistent across hardware with deltaTime
        movement = transform.TransformDirection(movement);  // Move the CharacterController
        cc.Move(movement);      // Move the CharacterController
    }

    // Unity's CharacterController will not work with Rigidbody, so physics collision must be simulated.
    // This event handler function is called when the the CharacterController's collider registers a hit.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!usePhysicsCollision)
            return;
        // Get the other collider's attached Rigidbody (using a shortcut here, but GetComponent<Rigidbody>() works too.)
        Rigidbody body = hit.collider.attachedRigidbody;

        // This will not work if there is no Rigidbody or it is set to Kinematic. If so, return; will skip the rest of code in this function. 
        if (body == null || body.isKinematic)
            return;

        // Filter out some small vertical collisions. return; will skip the rest of code in this function. 
        if (hit.moveDirection.y < -0.3F)
            return;

        // moveDirection will get the direction CharacterController was moving at time of impact. This determines the directions in which to apply forces.
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Apply physics and... PUSH!
        body.velocity = pushDirection * pushPower;
    }
}
