using UnityEngine;
using System.Collections;

public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public string horizontalAxis;
    public string verticalAxis;
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private Vector3 offset;



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void LateUpdate()
    {

    }
}
