using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionControl : MonoBehaviour
{
    // Get the Particle System component attached to this GameObject
    private ParticleSystem particles;

    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    // LateUpdate is run after Update
    void LateUpdate()
    {
        // If the Particle System has finished (can't be set to Loop), deactivate this GameObject
        if (!particles.IsAlive())
        {
            gameObject.SetActive(false);
        }
    }
}
