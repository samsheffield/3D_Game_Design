// Code Based on Unity documentation: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnControllerColliderHit.html
using UnityEngine;
using System.Collections;

public class PlayerPhysics : MonoBehaviour
{
    public float pushPower = 1f;

    // Unity's CharacterController will not work with a 
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;
        if (body == null || body.isKinematic)
            return;

        if (hit.moveDirection.y < -0.3F)
            return;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        body.velocity = pushDirection * pushPower;
    }
}
