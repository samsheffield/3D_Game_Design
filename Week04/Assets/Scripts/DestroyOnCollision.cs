using UnityEngine;
using System.Collections;

public class DestroyOnCollision : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
