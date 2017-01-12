using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour
{
    private float closedX, openX, targetX;
    public float openOffset = 2.1f;
    public float doorSpeed = 10f;
    public float closeDelay = 1f;

    void Start()
    {
        closedX = transform.position.x;
        openX = transform.position.x + openOffset;
        targetX = closedX;
    }

    void Update()
    {
        float currentX = Mathf.Lerp(transform.position.x, targetX, doorSpeed * Time.deltaTime);
        transform.position = new Vector3(currentX, transform.position.y, transform.position.z);
    }

    public void OpenDoor()
    {
        targetX = openX;
    }

    public void CloseDoor()
    {
        StartCoroutine(DelayedClose());
    }

    private IEnumerator DelayedClose()
    {
        yield return new WaitForSeconds(closeDelay);
        targetX = closedX;
    }
}
