// Basic transform rotation in Unity
using UnityEngine;
using System.Collections;

public class RotateGameObject : MonoBehaviour
{
    // The amount to rotate per frame
    public Vector3 rotate;

    void Update()
    {
        // Time.deltaTime is used to make the movement framerate independent
        transform.Rotate(rotate * Time.deltaTime);
    }
}
