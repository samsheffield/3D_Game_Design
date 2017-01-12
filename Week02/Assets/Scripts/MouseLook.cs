// MouseLook.cs code based on an example from Unity In Action: https://www.manning.com/books/unity-in-action
// Attach this component to your player GameObject and set axes field in the Inspector to MouseX.
// Also attach this component to a child Camera of your player and set the axes field to MouseY.

using UnityEngine;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    // Enumerations allow you to create a collection of related constants that can later be referred to by their members' tags: https://unity3d.com/learn/tutorials/topics/scripting/enumerations
    // If you intend to set an enum variable in the Inspector, then make the enum public
    public enum RotationAxes
    {
        MouseX = 0,
        MouseY = 1
    }

    // Set an enum to check which rotation code to use. Default value is RotationAxes.MouseX
    public RotationAxes axes = RotationAxes.MouseX;

    // Set the sensitivity of mouse movement
    public float sensitivityH = 9f;
    public float sensitivityV = 9f;

    // Limit the vertical camera rotation
    public float minimumV = -45f;
    public float maximumV = 45f;

    // Used to keep track of horizontal rotation
    private float rotationX;

    void Start()
    {
        // Check if there is an attached Rigidbody
        Rigidbody body = GetComponent<Rigidbody>();

        // If so, freeze rotation of the rigidbody
        if (body != null)
            body.freezeRotation = true;
    }

    void Update()
    {
        // Which enumeration member was selected? RotationAxes.MouseX = Horizontal rotation, RotationAxes.MouseY = Vertical rotation
        if (axes == RotationAxes.MouseX)
        {
            // Transform.Rotate expects x,y,z angles: https://docs.unity3d.com/ScriptReference/Transform.Rotate.html
            // Get the input from "Mouse X" entry in the Input Manager
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityH, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // Get the input from "Mouse Y" entry in the Input Manager
            rotationX -= Input.GetAxis("Mouse Y") * sensitivityV;

            // Mathf.Clamp returns a float which has minimum and maximum limits set: https://docs.unity3d.com/ScriptReference/Mathf.Clamp.html
            // rotationX is limited to a range between minimumV and maximumV
            rotationX = Mathf.Clamp(rotationX, minimumV, maximumV);

            // Transform.EulerAngles sets the rotation as Euler angles in degrees relative to the parent transform's rotation: https://docs.unity3d.com/ScriptReference/Transform-localEulerAngles.html
            transform.localEulerAngles = new Vector3(rotationX, transform.localEulerAngles.y, 0);
        }
    }
}
