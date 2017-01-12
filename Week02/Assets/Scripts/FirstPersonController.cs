// Original code based on an example from Unity in Action: https://www.manning.com/books/unity-in-action
using UnityEngine;
using System.Collections;

// A CharacterController component must be attached to this GameObject, so you can have Unity to add it like this
[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
    public float speed = 6f;        // How fast the player will move
    public float gravity = -9.8f;   // Simulated gravity

    public bool usePhysicsCollision = true;    // Interact with Rigidbodies? Note: CharacterController will not directly work with a Rigidbody component
    public float pushPower = 1f;        // How much force to apply when hitting a Rigidbody

    private CharacterController cc;     // Player's CharacterController component: https://docs.unity3d.com/Manual/class-CharacterController.html

    void Start()
    {
        // Get attached component
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Get Movement on the Horizontal & Vertical axes (set in Input Manager) and multiply by speed
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        // Create movement Vector3 based on change in input
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        // Limit the speed of movement with Vector3.ClampMagnitude: https://docs.unity3d.com/ScriptReference/Vector3.ClampMagnitude.html
        movement = Vector3.ClampMagnitude(movement, speed);

        // Apply simulated gravity to y axis
        movement.y = gravity;

        // Make the movement frame rate independent by multiplying with Time.deltaTime: https://unity3d.com/learn/tutorials/topics/scripting/delta-time
        movement *= Time.deltaTime;

        // Transform the movement direction from local to world space using Transform.TransformDirection: https://docs.unity3d.com/ScriptReference/Transform.TransformDirection.html
        movement = transform.TransformDirection(movement);

        // Call CharacterController's Move method. Move the player.
        cc.Move(movement);
    }

    // Override the CharacterController's OnControllerColliderHit event handler. More info on OnControllerCilliderHit: https://docs.unity3d.com/ScriptReference/ControllerColliderHit.html
    // This event is called when the CharacterController's collider is hit by another collider.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // If Rigidbody collision was not enabled, exit the loop and skip the rest of the code in this function using return
        if (!usePhysicsCollision)
            return;

        // Try to get a Rigidbody attached to the hit collider
        Rigidbody body = hit.collider.attachedRigidbody;

        // If there is no attached Rigidbody, or if it is set to Kinematic, exit the loop
        if (body == null || body.isKinematic)
            return;

        // Filter out small downward collisions on the y axis using return
        if (hit.moveDirection.y < -.3f)
            return;

        // Otherwise, set the direction of movement in a new Vector3
        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // Set the hit Rigidbody's velocity to the direction * force
        body.velocity = pushDirection * pushPower;
    }


}
