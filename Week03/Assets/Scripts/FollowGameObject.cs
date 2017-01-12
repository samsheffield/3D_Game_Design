// Basic fransform-based following in Unity (non-lerped)
using UnityEngine;
using System.Collections;

public class FollowGameObject : MonoBehaviour
{
    // GameObject to follow (set in Inspector)
    public GameObject target;

    // Fixed distance between this GameObject and the target GameObject
    private Vector3 offset;

    void Start()
    {
        // Get the difference between the two GameObjects when starting and use it as the offset
        offset = transform.position - target.transform.position;
    }

    // LateUpdate is called after Update. It's used here so that the script can be sure that the target's movement has been updated first
    void LateUpdate()
    {
        // Add the offset to the position of the target to position this GameObject
        transform.position = target.transform.position + offset;
    }
}
