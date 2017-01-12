using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{

    public DoorControl doorControl;
    private Vector3 closedPosition, openPosition;

    void Start()
    {
        openPosition = transform.position + transform.up * -.025f;
        closedPosition = transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorControl.OpenDoor();
            transform.localPosition = openPosition;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorControl.CloseDoor();
            transform.localPosition = closedPosition;
        }
    }
}
